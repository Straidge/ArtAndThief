using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

  public Texture backgroundTexture;
  public Rect backgroundArea;
  public Rect mainButtonArea;
  public GUIStyle mainButtonStyle;

  void OnGUI() {
    GUI.DrawTexture(backgroundArea, backgroundTexture);
    if(GUI.Button(mainButtonArea, "", mainButtonStyle)) {
      Debug.Log("Boutton cliqué");
    }
  }

	// Use this for initialization
	void Start () {
    float scaleX = 61 / 100f * Screen.width;
    float scaleY = 61 / 100f * Screen.height;
    float buttonWidth = 26 / 100f * Screen.width;
    float buttonHeight = 21 / 100f * Screen.height;

    backgroundArea = new Rect(0,0, Screen.width, Screen.height);
    mainButtonArea = new Rect(scaleX, scaleY, buttonWidth, buttonHeight);
	}

	// Update is called once per frame
	void Update () {

	}
}
