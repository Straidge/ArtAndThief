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
  private float buttonVelocityX;
  private float buttonVelocityY;
  private float buttonDelay;
  private float buttonAnimationDelay;

  void OnGUI() {
    GUI.DrawTexture(backgroundArea, backgroundTexture);

    if (Time.timeSinceLevelLoad > buttonDelay) {
      GUI.color = new Color(1, 1, 1, Mathf.Min(Time.timeSinceLevelLoad - buttonDelay, 1));
      GUI.DrawTexture(mainButtonArea, mainButtonTexture);
      if (GUI.Button(mainButtonArea, "", mainButtonStyle)) {
        Application.LoadLevel("Level1");
      }
    }
  }

	// Use this for initialization
	void Start () {
    buttonDelay = 1;
    buttonAnimationDelay = 0.5f;
    buttonPosX = 21 / 100f * Screen.width;
    buttonPosY = 77 / 100f * Screen.height;
    finalButtonPosX = 49 / 100f * Screen.width;
    finalButtonPosY = 59 / 100f * Screen.height;
    float buttonWidth = 39 / 100f * Screen.width;
    float buttonHeight = 25 / 100f * Screen.height;

    backgroundArea = new Rect(0,0, Screen.width, Screen.height);
    mainButtonArea = new Rect(buttonPosX, buttonPosY, buttonWidth, buttonHeight);
	}

	// Update is called once per frame
	void Update () {
    // Delay one second before animation
    if (Time.timeSinceLevelLoad > buttonDelay) {
      buttonPosX = Mathf.SmoothDamp(buttonPosX, finalButtonPosX, ref buttonVelocityX, buttonAnimationDelay);
      buttonPosY = Mathf.SmoothDamp(buttonPosY, finalButtonPosY, ref buttonVelocityY, buttonAnimationDelay);
      mainButtonArea = new Rect(buttonPosX, buttonPosY, mainButtonArea.width, mainButtonArea.height);
    }
	}
}
