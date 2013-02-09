using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {
  private int score;

  public void addScore(int scoreToAdd) {
    score += scoreToAdd;
  }

  void Start() {
    score = 0;
  }

  void Update() {
    Debug.Log("Score : " + score);
  }

  public int getScore() {
    return score;
  }
}