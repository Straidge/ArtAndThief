using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BotController : MonoBehaviour {

	public CharacterController controller;
	public float Nspeed;
	public float Aspeed;
	public Vector3 botDir;
	public Vector3 dir;
	private Transform tr;
	public GameObject ThePath;
	private Transform player;
	private int rank;
	
	public LayerMask wallLayer;
	
	public float distanceToFocus;
	
	public GameObject anim;
	
	private enum Type {
		Search,
		Focus,
		WTF,
		Nevermind
	}
	private Type type;
	
	public List<Transform> myPath = new List<Transform>();
	private float posY;
	
	public GameObject exclamation;
	
	public List<AudioClip> stupAudio = new List<AudioClip>();


	void Start () {
		player = GameObject.Find("Player - Voleur").transform;
		controller = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();
		myPath = ThePath.GetComponent<PathGarde>().pathGame;
		SetTarget();
		posY = transform.position.y;
	}
	
	void SetTarget () {
		rank = 0;
		for (int i = 0; i < myPath.Count; i++) {
			if (Vector3.Distance(transform.position, myPath[i].position) < Vector3.Distance(transform.position, myPath[rank % myPath.Count].position)) {
				rank = i;
				Debug.Log ("Finded");
			}
		}
	}
	
	bool Visible () {
		RaycastHit hit;
		if (!Physics.Raycast(transform.position, (player.position - transform.position).normalized, out hit, Vector3.Distance(transform.position, player.position),wallLayer)) {
			return true;
		}
		return false;
	}
	
	// Update is called once per frame
	void Update () {
		
		transform.position = new Vector3(transform.position.x, posY, transform.position.z);
		
		if (type == Type.Search) {
			if (Vector3.Distance(transform.position, myPath[rank % myPath.Count].position) < 1) {
				rank++;
			}
			tr.LookAt(new Vector3(myPath[rank % myPath.Count].position.x, tr.position.y, myPath[rank % myPath.Count].position.z));
			controller.Move(tr.forward * Nspeed * Time.deltaTime);
			// Ronde to Focus
			if (Vector3.Distance(transform.position, player.position) < 10 && Vector3.Dot(transform.forward, player.position - transform.position) > 0 && !player.GetComponent<PlayerController>().hide) {
				if (Visible())
					StartCoroutine(GoWTF());
			}
		}
		
		if (type == Type.Focus) {
			tr.LookAt(new Vector3(player.position.x, tr.position.y, player.position.z));
			controller.Move(tr.forward * Nspeed * 1.2f * Time.deltaTime);
			GetComponent<FireController>().Fire();
			
			if (Vector3.Distance(transform.position, player.position) > 10 || Vector3.Dot(transform.forward, player.position - transform.position) < 0 || player.GetComponent<PlayerController>().hide || !Visible()) {
				StartCoroutine(GoNevermind());
			}
		}
		
		if (type == Type.Nevermind) {
			controller.Move(tr.forward * Nspeed * 1.2f * Time.deltaTime);
		}
		
		if (Vector3.Dot(tr.forward, Vector3.right) > 0) {
			anim.GetComponent<GardienAnimator>().sens = 1;
			if (type == Type.WTF) {
				anim.renderer.material.mainTextureOffset = new Vector2(-0.3f, 3);
				anim.GetComponent<GardienAnimator>().enabled = false;
			}
			else
				anim.GetComponent<GardienAnimator>().enabled = true;
		}
		else {
			anim.GetComponent<GardienAnimator>().sens = -1;
			if (type == Type.WTF) {
				anim.renderer.material.mainTextureOffset = new Vector2(0.3f, 3);
				anim.GetComponent<GardienAnimator>().enabled = false;
			}
			else
				anim.GetComponent<GardienAnimator>().enabled = true;
		}
		anim.transform.rotation = anim.GetComponent<GardienAnimator>().rot;
	}
	
	IEnumerator GoWTF() {
		GameObject clone = Instantiate(exclamation, transform.position + Vector3.up * 3, exclamation.transform.rotation) as GameObject;
		int test = Mathf.RoundToInt(Random.Range(0,9));
		Debug.Log(test);
		audio.PlayOneShot(stupAudio[test]);
		Debug.Log (stupAudio[test]);
		Destroy (clone, 1);
		tr.LookAt(new Vector3(player.position.x, tr.position.y, player.position.z));
		type = Type.WTF;
		yield return new WaitForSeconds(1F);
		if (Vector3.Dot(transform.forward, player.position - transform.position) > 0 && !player.GetComponent<PlayerController>().hide && Visible ()) {
			type = Type.Focus;
		}
		else
			type = Type.Search;
	}
	
	IEnumerator GoNevermind() {
		type = Type.Nevermind;
		yield return new WaitForSeconds(1F);
		if (Vector3.Dot(transform.forward, player.position - transform.position) > 0 && !player.GetComponent<PlayerController>().hide && Visible ()) {
			type = Type.Focus;
			audio.PlayOneShot(stupAudio[Mathf.RoundToInt(Random.Range(0,9))]);
			GameObject clone = Instantiate(exclamation, transform.position + Vector3.up * 3, exclamation.transform.rotation) as GameObject;
			Destroy (clone, 1);
		}
		else
			type = Type.Search;
	}
	
}