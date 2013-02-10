using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour
{

	public Texture backgroundTexture;
	public Rect backgroundArea;
	public Rect mainButtonArea;
	public Texture mainButtonTexture;
	public GUIStyle mainButtonStyle;
	public float buttonPosX;
	public float buttonPosY;
	public float finalButtonPosY;
	public float finalButtonPosX;
	private float buttonVelocityX;
	private float buttonVelocityY;
	private float buttonDelay;
	private float buttonAnimationDelay;
	public Texture paramTexture;
	public Rect paramArea;
	public float paramPosY;
	public float finalParamPosY;
	private float paramVelocityY;
	public Texture fadeur;
	private bool goFade;
	private float tFade;
	public Texture credz;
	public bool credzOn;
	public GUIStyle buttonStyle;
	private float credT;

	void OnGUI ()
	{
		GUI.DrawTexture (backgroundArea, backgroundTexture);

		if (Time.timeSinceLevelLoad > buttonDelay) {
			GUI.DrawTexture (paramArea, paramTexture);
			if (GUI.Button (paramArea, "", buttonStyle)) {
				credzOn = true;
			}



			// Add fade-in to "Let's go button"
			GUI.color = new Color (1, 1, 1, Mathf.Min (Time.timeSinceLevelLoad - buttonDelay, 1));
			GUI.DrawTexture (mainButtonArea, mainButtonTexture);
			if (GUI.Button (mainButtonArea, "", mainButtonStyle)) {
				StartCoroutine (LoadDatLevel (2));
			}
			
			GUI.color = new Color (1, 1, 1, tFade);
			GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), fadeur);
		}
		
		
		GUI.color = new Color (1, 1, 1, credT);
		GUI.DrawTexture (new Rect (0, 0, Screen.width, Screen.height), credz);
		if (GUI.Button (new Rect (0, 0, Screen.width, Screen.height), "", buttonStyle)) {
			credzOn = false;
		}
		
		
	}
	
	IEnumerator LoadDatLevel (int sceneNumber)
	{
		goFade = true;
		yield return new WaitForSeconds(1f);
		Application.LoadLevel (sceneNumber);
	}

	// Use this for initialization
	void Start ()
	{
		buttonDelay = 0.5f;
		buttonAnimationDelay = 0.5f;
		buttonPosX = 21 / 100f * Screen.width;
		buttonPosY = 77 / 100f * Screen.height;
		finalButtonPosX = 49 / 100f * Screen.width;
		finalButtonPosY = 59 / 100f * Screen.height;
		float buttonWidth = 39 / 100f * Screen.width;
		float buttonHeight = 25 / 100f * Screen.height;

		float paramPosX = 74 / 100f * Screen.width;
		paramPosY = -21 / 100f * Screen.height;
		finalParamPosY = 0;
		float paramWidth = 22 / 100f * Screen.width;
		float paramHeight = 28 / 100f * Screen.height;

		backgroundArea = new Rect (0, 0, Screen.width, Screen.height);
		mainButtonArea = new Rect (buttonPosX, buttonPosY, buttonWidth, buttonHeight);
		paramArea = new Rect (paramPosX, paramPosY, paramWidth, paramHeight);
	}

	// Update is called once per frame
	void Update ()
	{
		
		if (credzOn)
			credT += Time.deltaTime;
		else
			credT -= Time.deltaTime;
		
		credT = Mathf.Clamp (credT, 0, 1);
		
		if (credzOn) {
			if (Input.GetKeyDown (KeyCode.Escape))
				credzOn = false;
		}
		else {
			if (Input.GetKeyDown (KeyCode.Escape))
				Application.Quit();
		}
		
		if (goFade)
			tFade += Time.deltaTime;
		// Delay one second before animation
		if (Time.timeSinceLevelLoad > buttonDelay) {
			// Animate "Let's go" button
			buttonPosX = Mathf.SmoothDamp (buttonPosX, finalButtonPosX, ref buttonVelocityX, buttonAnimationDelay);
			buttonPosY = Mathf.SmoothDamp (buttonPosY, finalButtonPosY, ref buttonVelocityY, buttonAnimationDelay);
			mainButtonArea = new Rect (buttonPosX, buttonPosY, mainButtonArea.width, mainButtonArea.height);

			// Animate "Parameters" button
			paramPosY = Mathf.SmoothDamp (paramPosY, finalParamPosY, ref paramVelocityY, buttonAnimationDelay);
			paramArea = new Rect (paramArea.x, paramPosY, paramArea.width, paramArea.height);

		}
	}
}
