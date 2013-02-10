using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
	public float score;
	
	public bool hide;
	
	private enum State {
		Play,
		Winn,
		Lose,
		Tuto,
		Pause
	}
	private State Etat;
	public Texture JoyPadOn;
	public Texture JoyPadOff;
	public Texture CatchPad;
	
	private bool onJoypadPush;
	
	public GameObject particlesCaught;
	
	public GUIStyle buttonStyle;
	
	public GameObject anim;
	
	public GameObject scoring;
	
	private bool uLose;
	private float uLoseT;
	public Texture youLose;
	private Rect youLoseArea;
	private float youLoseOffsetY;
	private float youLoseVelocityY;
	
	public Texture youWin;
	private Rect youWinnArea;
	private float youWinnOffsetY;
	private float youWinnVelocityY;
	
	public Texture fadeur;
	
	public List<Texture> stealedReward = new List<Texture>();
	public List<float> valeur = new List<float>();
	public List<Rect> stealedRewardArea = new List<Rect>();
	
	private float winnT;
	
	public Texture pauseButton;
	
	public AudioClip gameOverClip;
	
	public Texture restart;
	public Texture menu;
	
	private Texture tuto;
	private float tutoT;
	
	public Texture ecranPause;
	public Texture pauseMenu;
	public Texture pauseResume;
	
	public Texture uWinnNext;
	public Texture uWinnRestart;
	public Texture uWinnMenu;



	void Start () {
		Instantiate(scoring, new Vector3(0, 100, 0), Quaternion.identity);
		Camera.main.transform.GetComponent<CamController>().cible = transform;
		Etat = State.Play;
		controller = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();
		joyRect = new Rect ( 5/100f * Screen.width, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
		actionRect = new Rect ( Screen.width - 1/10f * Screen.width - 30/100f * Screen.height, 60/100f * Screen.height, 30/100f * Screen.height, 30/100f * Screen.height);
	
		youLoseArea = new Rect (20/100f * Screen.width, -75/100F * Screen.height, 60/100f * Screen.width, 90/100F * Screen.height);
	}
	
	// Update is called once per frame
	void Update () {
		
		tutoT += Time.deltaTime/Time.timeScale;
		
		
		
		if (uLose)
			uLoseT += Time.deltaTime;
		
		joyRect = new Rect ( 3/100f * Screen.width, 60/100f * Screen.height, 40/100f * Screen.height, 40/100f * Screen.height);
		actionRect = new Rect ( Screen.width - 5/100f * Screen.width - 30/100f * Screen.height, 60/100f * Screen.height, 35/100f * Screen.height, 35/100f * Screen.height);
	#if UNITY_EDITOR
		dir = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		tr.LookAt(tr.position + dir);
		Aspeed = Mathf.Min(Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")), 1) * Nspeed;

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
		
		if (Etat == State.Winn) {
			winnT += Time.deltaTime;
		}
	}
	
	Vector3 GetAxe () {
		
		if (onJoypadPush) {
			for (int i = 0; i < Input.touchCount; i++) {
				Vector2 fingerPos = new Vector2 (Input.GetTouch(i).position.x, Screen.height - Input.GetTouch(i).position.y);
				if (new Rect(0, 0, Screen.width/2f, Screen.height).Contains(fingerPos)) {
					Vector2 sens = (fingerPos - new Vector2 (joyRect.x + joyRect.width/2f, joyRect.y + joyRect.height/2f)).normalized;
					
					//if (Mathf.Abs(sens.x) > Mathf.Abs(sens.y))
					//	sens = new Vector2(sens.x,0).normalized;
					//else
					//	sens = new Vector2(0,sens.y).normalized;
						
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
				
				//if (Mathf.Abs(sens.x) > Mathf.Abs(sens.y))
				//	sens = new Vector2(sens.x,0).normalized;
				//else
				//	sens = new Vector2(0,sens.y).normalized;
					
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
		
		if (Etat != State.Tuto) {
			#if UNITY_EDITOR
			
			#else
			if (Etat != State.Lose && Etat != State.Winn) {
				if (onJoypadPush)
					GUI.DrawTexture(joyRect, JoyPadOn);
				else
					GUI.DrawTexture(joyRect, JoyPadOff);
			}
			#endif
			
			if (nearObject && !item.GetComponent<ItemToSteal>().caught) {
				if (Etat != State.Winn && Etat != State.Lose && Etat != State.Pause)
					GUI.DrawTexture(actionRect, CatchPad);
				if (GUI.Button(actionRect, "", buttonStyle)) {
					if (item.GetComponent<TutoController>()) {
						
						
							Etat = State.Tuto;
							tuto = item.GetComponent<TutoController>().tuto;
							item.GetComponent<TutoController>().caught = true;
							tutoT = 0;
							Time.timeScale = 0.000000001f;
						
						
						
					}
					item.GetComponent<ItemToSteal>().caught = true;
					Instantiate(particlesCaught, item.transform.position, item.transform.rotation);
					score += item.GetComponent<ItemToSteal>().valeur;
					if (item.renderer.material.mainTexture != null)
						stealedReward.Add(item.renderer.material.mainTexture);
					GameObject.Find("Score Text").GetComponent<TextMesh>().text = score.ToString();
				}
			}
			else {
				GUI.color = new Color (1,1,1,0.4f);
				if (Etat != State.Winn && Etat != State.Lose && Etat != State.Pause)
				GUI.DrawTexture(actionRect, CatchPad);
			}
			//GUI.Label(new Rect(0,0, Screen.width, Screen.height), score.ToString());
			
			GUI.color = Color.white;
			if (Etat == State.Lose) {
				youLoseOffsetY = Mathf.SmoothDamp(youLoseOffsetY, 75/100F * Screen.height, ref youLoseVelocityY, 1);
				youLoseArea = new Rect (15/100f * Screen.width, -75/100F * Screen.height + youLoseOffsetY, 50/100f * Screen.width, 67/100F * Screen.height);
				GUI.DrawTexture(youLoseArea, youLose);
				//if (GUI.Button(youLoseArea, "", buttonStyle)) {
					//uLose = true;
					//StartCoroutine(LoadMainScreen(1));
				//}
				
				Rect MenuArea = new Rect(15/100f * Screen.width, youLoseArea.y + 65/100f * Screen.height, 25/100f * Screen.width, 20/100f * Screen.height);
				GUI.DrawTexture(MenuArea, menu);
				if (GUI.Button(MenuArea, "", buttonStyle)) {
					Debug.Log("Menu Area");
					uLose = true;
					StartCoroutine(LoadMainScreen(1));
				}
				
				Rect restartArea = new Rect(41/100f * Screen.width, youLoseArea.y + 60.5f/100f * Screen.height, 25/100f * Screen.width, 20/100f * Screen.height);
				GUI.DrawTexture(restartArea, restart);
				if (GUI.Button(restartArea, "", buttonStyle)) {
					uLose = true;
					Debug.Log("Restart : " + Application.loadedLevelName);
					StartCoroutine(LoadWithName(Application.loadedLevelName));
				}
			}
			
			if (Etat == State.Winn) {
				youLoseOffsetY = Mathf.SmoothDamp(youLoseOffsetY, 75/100F * Screen.height, ref youLoseVelocityY, 1);
				youLoseArea = new Rect (7/100f * Screen.width, -85/100F * Screen.height + youLoseOffsetY, 60/100f * Screen.width, 80/100F * Screen.height);
				GUI.DrawTexture(youLoseArea, youWin);
				if (GUI.Button(youLoseArea, "", buttonStyle)) {
					//uLose = true;
					//StartCoroutine(LoadMainScreen());
				}
				Matrix4x4 myScreen = GUI.matrix;
				for (int i = 0; i < valeur.Count; i++) {
					GUI.color = new Color(1,1,1, Mathf .Clamp(winnT - (1.3f + 0.3f * i), 0, 1));
					GUI.matrix = myScreen;
					GUIUtility.ScaleAroundPivot(Vector2.one * Mathf.Max(1, ((2.5f + 0.3f * i) - winnT) * 5f), stealedRewardArea[i].center);
					GUIUtility.RotateAroundPivot(valeur[i], stealedRewardArea[i].center);
					GUI.DrawTexture(stealedRewardArea[i], stealedReward[i]);
				}
				
				GUI.matrix = myScreen;
				GUI.color = Color.white;
				
				Rect uWinnRestartArea = new Rect(7/100f * Screen.width, -5/100f * Screen.height + youLoseOffsetY, 28/100f * Screen.width, 23/100f * Screen.height);
				GUI.DrawTexture(uWinnRestartArea, uWinnRestart);
				if (GUI.Button(uWinnRestartArea, "", buttonStyle)) {
					StartCoroutine(LoadMainScreen(Application.loadedLevel));
					uLose = true;
				}
				
				Rect uWinnMenuArea = new Rect(36/100f * Screen.width, -8/100f * Screen.height + youLoseOffsetY, 22/100f * Screen.width, 23/100f * Screen.height);
				GUI.DrawTexture(uWinnMenuArea, uWinnMenu);
				if (GUI.Button(uWinnMenuArea, "", buttonStyle)) {
					Debug.Log("Menu");
					StartCoroutine(LoadMainScreen(1));
					uLose = true;
				}
				
				if (Application.loadedLevel + 1 < Application.levelCount) {
					
				}
				else
					GUI.color = new Color(1,1,1,0.5f);
				Rect uWinnNextArea = new Rect(59/100f * Screen.width, -12/100f * Screen.height + youLoseOffsetY, 22/100f * Screen.width, 23/100f * Screen.height);
				GUI.DrawTexture(uWinnNextArea, uWinnNext);
				if (Application.loadedLevel + 1 < Application.levelCount) {
					if (GUI.Button(uWinnNextArea, "", buttonStyle)) {
						Debug.Log(Application.levelCount.ToString() + "    " + Application.loadedLevel.ToString());
						Debug.Log ("WTF??");
						StartCoroutine(LoadMainScreen(Application.loadedLevel + 1));
						uLose = true;
					}
				}
				GUI.color = Color.white;
				
				
			}
			
			if (uLose) {
				GUI.color = new Color(1,1,1,uLoseT);
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeur);
			}
			
			if (Time.timeSinceLevelLoad <1) {
				GUI.color = new Color(1,1,1, 1 - Time.timeSinceLevelLoad);
				GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeur);
			}
			
			GUI.color = Color.white;
			if (Etat == State.Play) {
				Rect pauseRect = new Rect(90/100f * Screen.width, 0, 10/100f * Screen.width, 10/100f * Screen.width);
				GUI.DrawTexture(pauseRect, pauseButton);
				if (GUI.Button(pauseRect, "", buttonStyle) || Input.GetKeyDown(KeyCode.Escape)) {
					Etat = State.Pause;
					Time.timeScale = 0.000001f;
				}
			}

			
			if (Etat == State.Pause) {
				
				Rect ecranPauseArea = new Rect(Screen.width/2f - 20/100F * Screen.height, 20/100F * Screen.height, 40/100F * Screen.height, 40/100F * Screen.height);
				GUI.DrawTexture(ecranPauseArea, ecranPause);
				
				Rect pauseResumeArea = new Rect(55/100f * Screen.width, 60/100F * Screen.height, 25/100F * Screen.width, 20/100F * Screen.height);
				GUI.DrawTexture(pauseResumeArea, pauseResume);
				if (GUI.Button(pauseResumeArea, "" ,buttonStyle)) {
					Etat = State.Play;
					Time.timeScale = 1f;
				}
				
				Rect pauseMenuArea = new Rect(45/100f * Screen.width - 25/100F * Screen.width, 60/100F * Screen.height, 25/100F * Screen.width, 20/100F * Screen.height);
				GUI.DrawTexture(pauseMenuArea, pauseMenu);
				if (GUI.Button(pauseMenuArea, "" ,buttonStyle)) {
					Time.timeScale = 1f;
					uLose = true;
					StartCoroutine(LoadMainScreen(1));
					Debug.Log("PausemenuArea");
				}
			}
		}
		else {
			Rect tutoR = new Rect(0,  Screen.height - Screen.width/2.3f, Screen.width, Screen.width/2.3f);
			GUI.DrawTexture(tutoR, tuto);
			if (GUI.Button(tutoR, "", buttonStyle)) {
				if (tutoT > 1) {
					Etat = State.Play;
					Time.timeScale = 1;
				}
			}
		}
	}
	
	IEnumerator LoadMainScreen (int datScene) {
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(datScene);
	}
	
	IEnumerator LoadWithName (string name) {
		yield return new WaitForSeconds(1f);
		Application.LoadLevel(name);
	}
	
	void SetGUIReward () {
		
		for (int i = 0; i < stealedReward.Count; i++) {
			stealedRewardArea.Add(new Rect(12/100f * Screen.width + 5/100f * Screen.width * i, 50/100f * Screen.height - 1/100f * Screen.height * i, 20/100f * Screen.height, (20/100f * Screen.height) * (stealedReward[i].height/stealedReward[i].width)));
			valeur.Add(Random.Range(-30, 30));
		}
		
	}
	
	public void Lose () {
		if (Etat != State.Lose)
			Camera.main.transform.GetComponent<SoundController>().SetMusic(gameOverClip);
		Etat = State.Lose;
	}
	
	void OnTriggerEnter (Collider other) {
		if (other.tag == "Exit" && score > 0) {
			SetGUIReward();
			Etat = State.Winn;
		}
		if (other.tag == "Tuto") {
			if (!other.GetComponent<TutoController>().caught) {
				Etat = State.Tuto;
				tuto = other.GetComponent<TutoController>().tuto;
				other.GetComponent<TutoController>().caught = true;
				tutoT = 0;
				Time.timeScale = 0.000000001f;
			}
		}
	}
}
