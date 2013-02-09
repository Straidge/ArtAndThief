using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathGarde : MonoBehaviour {
	
	public List<Transform> pathGame = new List<Transform>();
	
	[ContextMenu("Set Path fffffUUUUUU")]
	void SetPath () {
		pathGame = new List<Transform>();
		foreach (Transform child in transform) {
			pathGame.Add(child);
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnDrawGizmosSelected() {
		for (int i = 0; i < pathGame.Count; i++) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(pathGame[i].position, pathGame[(i + 1) % pathGame.Count].position);
			
		}
	}
}
