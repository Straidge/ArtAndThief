using UnityEngine;
using System.Collections;

public class SplashScreen : MonoBehaviour {

  public Texture backgroundTexture;
  public Rect backgroundArea;
  public Rect mainButtonArea;
  public Texture mainButtonTexture;
  public GUIStyle mainButtonStyle;

  void OnGUI() {
    GUI.DrawTexture(backgroundArea, backgroundTexture);
    GUI.DrawTexture(mainButtonArea, mainButtonTexture);
    if(GUI.Button(mainButtonArea, "", mainButtonStyle)) {
      Debug.Log("Boutton cliqué");
    }
  }

	// Use this for initialization
	void Start () {
    float posX = 46 / 100f * Screen.width;
    float posY = 59 / 100f * Screen.height;
    float buttonWidth = 39 / 100f * Screen.width;
    float buttonHeight = 25 / 100f * Screen.height;

    backgroundArea = new Rect(0,0, Screen.width, Screen.height);
    mainButtonArea = new Rect(posX, posY, buttonWidth, buttonHeight);
	}

	// Update is called once per frame
	void Update () {

	}
}
