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
		Play,
		Winn,
		Lose
	}
	private State Etat;
	public Texture JoyPadOn;
	public Texture JoyPadOff;
	public Texture CatchPad;
	
	private bool onJoypadPush;
	
	public GameObject particlesCaught;
	
	public GUIStyle buttonStyle;
	
	public GameObject anim;
	



	void Start () {
		Camera.main.transform.GetComponent<CamController>().cible = transform;
		Etat = State.Play;
		controller = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();
		joyRect = new Rect ( 1/10f * Screen.width, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
		actionRect = new Rect ( Screen.width - 1/10f * Screen.width - 30/100f * Screen.height, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		joyRect = new Rect ( 1/10f * Screen.width, 60/100f * Screen.height, 40/100f * Screen.height, 40/100f * Screen.height);
		actionRect = new Rect ( Screen.width - 1/10f * Screen.width - 30/100f * Screen.height, 60/100f * Screen.height, 35/100f * Screen.height, 35/100f * Screen.height);
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
		
		
		
		if (Etat == State.Play)
			controller.Move(tr.forward * Aspeed * Time.deltaTime);
		
		anim.transform.rotation = anim.GetComponent<VoleurAnimator>().rot;
		
		
		if (Vector3.Dot(tr.forward, Vector3.right) > 0) {
			anim.GetComponent<VoleurAnimator>().sens = 1;
			if (Aspeed == 0) {
				anim.renderer.material.mainTextureOffset = new Vector2(-0.3f, 3);
				anim.GetComponent<VoleurAnimator>().enabled = false;
			}
			else
				anim.GetComponent<VoleurAnimator>().enabled = true;
			
		}
		else {
			anim.GetComponent<VoleurAnimator>().sens = -1;
			if (Aspeed == 0) {
				anim.renderer.material.mainTextureOffset = new Vector2(0.3f, 3);
				anim.GetComponent<VoleurAnimator>().enabled = false;
			}
			else
				anim.GetComponent<VoleurAnimator>().enabled = true;
			
			
		}
	}
	
	Vector3 GetAxe () {
		
		if (onJoypadPush) {
			for (int i = 0; i < Input.touchCount; i++) {
				Vector2 fingerPos = new Vector2 (Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y);
				if (new Rect(0, 0, Screen.width/2f, Screen.height).Contains(fingerPos)) {
					Vector2 sens = (fingerPos - new Vector2 (joyRect.x + joyRect.width/2f, joyRect.y + joyRect.height/2f)).normalized;
					
					if (Mathf.Abs(sens.x) > Mathf.Abs(sens.y))
						sens = new Vector2(sens.x,0).normalized;
					else
						sens = new Vector2(0,sens.y).normalized;
						
					onJoypadPush = true;
					return new Vector3 (sens.x, 0, -sens.y);
				}
			}
		}
		else {
			for (int i = 0; i < Input.touchCount; i++) {
			Vector2 fingerPos = new Vector2 (Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y);
			if (joyRect.Contains(fingerPos)) {
				Vector2 sens = (fingerPos - new Vector2 (joyRect.x + joyRect.width/2f, joyRect.y + joyRect.height/2f)).normalized;
				
				if (Mathf.Abs(sens.x) > Mathf.Abs(sens.y))
					sens = new Vector2(sens.x,0).normalized;
				else
					sens = new Vector2(0,sens.y).normalized;
					
				onJoypadPush = true;
				return new Vector3 (sens.x, 0, -sens.y);
				}
			}
		}
		onJoypadPush = false;
		return Vector3.zero;
		
	}
	
	bool HoverJoyPad () {
		for (int i = 0; i < Input.touchCount; i++) {
			Vector2 fingerPos = new Vector2 (Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y);
			if (joyRect.Contains(fingerPos)) {
				return true;
			}	
		}
		return false;
	}
	
	
	void OnGUI () {
		#if UNITY_EDITOR
		
		#else
		if (onJoypadPush)
			GUI.DrawTexture(joyRect, JoyPadOn);
		else
			GUI.DrawTexture(joyRect, JoyPadOff);
		#endif
		
		if (nearObject && Vector3.Dot(tr.forward, item.transform.position - tr.position) > 0 && !item.GetComponent<ItemToSteal>().caught) {
			if (GUI.Button(actionRect, CatchPad, buttonStyle)) {
				item.GetComponent<ItemToSteal>().caught = true;
				Instantiate(particlesCaught, item.transform.position, item.transform.rotation);
				score ++;
			}
		}
		GUI.Label(new Rect(0,0, Screen.width, Screen.height), score.ToString());
	}
	
	public void Lose () {
		Etat = State.Lose;
		StartCoroutine(WaitToLose());
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Exit" && score > 0) {
			Etat = State.Winn;
			StartCoroutine(WaitToReload());
		}
	}
	
	IEnumerator WaitToLose () {
		yield return new WaitForSeconds(2f);
		Application.LoadLevel(1);
	}
	
	IEnumerator WaitToReload () {
		yield return new WaitForSeconds(2f);
		Application.LoadLevel(1);
	}
}
