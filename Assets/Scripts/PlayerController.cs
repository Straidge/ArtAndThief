using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public CharacterController controller;
	public float Nspeed;
	private float Aspeed;
	private Vector3 dir;
	private Transform tr;
	private Rect joyRect;
	private Rect actionRect;
	public bool nearObject;
	public GameObject item;
	public int score;
	
	public bool hide;
	
	private enum State {
		Voleur,
		Gardien
	}
	private State Etat;
	public Texture JoyPad;


	void Start () {
		controller = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();
		joyRect = new Rect ( 1/10f * Screen.width, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
		actionRect = new Rect ( Screen.width - 1/10f * Screen.width - 30/100f * Screen.height, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		joyRect = new Rect ( 1/10f * Screen.width, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
		actionRect = new Rect ( Screen.width - 1/10f * Screen.width - 30/100f * Screen.height, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
	#if UNITY_EDITOR
		dir = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		tr.LookAt(tr.position + dir);
		Aspeed = Mathf.Min(Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")), 1) * Nspeed;
		if (Input.GetKeyDown(KeyCode.Space))
			GetComponent<FireController>().Fire();
	#else
		dir = GetAxe();
		tr.LookAt(tr.position + dir);
		Aspeed = Mathf.Min(Mathf.Abs(dir.x) + Mathf.Abs(dir.z), 1) * Nspeed;
	#endif
		controller.Move(tr.forward * Aspeed * Time.deltaTime);
	}
	
	Vector3 GetAxe () {
		for (int i = 0; i < Input.touchCount; i++) {
			Vector2 fingerPos = new Vector2 (Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y);
			if (joyRect.Contains(fingerPos)) {
				Vector2 sens = (fingerPos - new Vector2 (joyRect.x + joyRect.width/2f, joyRect.y + joyRect.height/2f)).normalized;
				
				if (Mathf.Abs(sens.x) > Mathf.Abs(sens.y))
					sens = new Vector2(sens.x,0).normalized;
				else
					sens = new Vector2(0,sens.y).normalized;
				
				return new Vector3 (sens.x, 0, -sens.y);
			}	
		}
		return Vector3.zero;
	}
	
	
	void OnGUI () {
		GUI.DrawTexture(joyRect, JoyPad);
		if (nearObject && Vector3.Dot(tr.forward, item.transform.position - tr.position) > 0 && !item.GetComponent<ItemToSteal>().caught) {
			if (GUI.Button(actionRect, JoyPad)) {
				item.GetComponent<ItemToSteal>().caught = true;
				score ++;
			}
		}
		GUI.Label(new Rect(0,0, Screen.width, Screen.height), score.ToString());
	}
}
