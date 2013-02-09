using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
  private int score;
  private Vector3 position;
  public GameObject scoreDisplay;

  public void addScore(int scoreToAdd) {
    score += scoreToAdd;
  }

  void Start() {
    score = 0;
    position = new Vector3(0, 100, 0);
    Instantiate(scoreDisplay, position, Quaternion.identity);
  }

  void Update() {
    GameObject.Find("Score Value").GetComponent<TextMesh>().text = score.ToString();
    Debug.Log("Score : " + score);
  }

  public int getScore() {
    return score;
  }
}