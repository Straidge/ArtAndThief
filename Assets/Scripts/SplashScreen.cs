using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

  public Texture backgroundTexture;
  public Rect backgroundArea;
  public Rect mainButtonArea;
  public Texture mainButtonTexture;
  public GUIStyle mainButtonStyle;
  public float buttonPosX;
  public float buttonPosY;
  public float finalButtonPosY;
  public float finalButtonPosX;
  public Texture paramTexture;
  public Rect paramArea;
  public float paramPosY;
  public float finalParamPosY;

  private float buttonVelocityX;
  private float buttonVelocityY;
  private float buttonDelay;
  private float buttonAnimationDelay;
  private float paramVelocityY;

  void OnGUI() {
    GUI.DrawTexture(backgroundArea, backgroundTexture);

    if (Time.timeSinceLevelLoad > buttonDelay) {
      GUI.DrawTexture(paramArea, paramTexture);

      // Add fade-in to "Let's go button"
      GUI.color = new Color(1, 1, 1, Mathf.Min(Time.timeSinceLevelLoad - buttonDelay, 1));
      GUI.DrawTexture(mainButtonArea, mainButtonTexture);
      if (GUI.Button(mainButtonArea, "", mainButtonStyle)) {
        Application.LoadLevel("Level1");
      }
    }
  }

	// Use this for initialization
	void Start () {
    buttonDelay = 0.5f;
    buttonAnimationDelay = 0.5f;
    buttonPosX = 21 / 100f * Screen.width;
    buttonPosY = 77 / 100f * Screen.height;
    finalButtonPosX = 49 / 100f * Screen.width;
    finalButtonPosY = 59 / 100f * Screen.height;
    float buttonWidth = 39 / 100f * Screen.width;
    float buttonHeight = 25 / 100f * Screen.height;

    float paramPosX = 67 / 100f * Screen.width;
    paramPosY = -21 / 100f * Screen.height;
    finalParamPosY = 0;
    float paramWidth = 15 / 100f * Screen.width;
    float paramHeight = 21 / 100f * Screen.height;

    backgroundArea = new Rect(0,0, Screen.width, Screen.height);
    mainButtonArea = new Rect(buttonPosX, buttonPosY, buttonWidth, buttonHeight);
    paramArea = new Rect(paramPosX, paramPosY, paramWidth, paramHeight);
	}

	// Update is called once per frame
	void Update () {
    // Delay one second before animation
    if (Time.timeSinceLevelLoad > buttonDelay) {
      // Animate "Let's go" button
      buttonPosX = Mathf.SmoothDamp(buttonPosX, finalButtonPosX, ref buttonVelocityX, buttonAnimationDelay);
      buttonPosY = Mathf.SmoothDamp(buttonPosY, finalButtonPosY, ref buttonVelocityY, buttonAnimationDelay);
      mainButtonArea = new Rect(buttonPosX, buttonPosY, mainButtonArea.width, mainButtonArea.height);

      // Animate "Parameters" button
      paramPosY = Mathf.SmoothDamp(paramPosY, finalParamPosY, ref paramVelocityY, buttonAnimationDelay);
      paramArea = new Rect(paramArea.x, paramPosY, paramArea.width, paramArea.height);
    }
	}
}
