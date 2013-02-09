using UnityEngine;
using System.Collections;

public class StartLoader : MonoBehaviour {

	// Use this for initialization
	void Start () {
		StartCoroutine(LoadDatScene());
	
	}
	
	IEnumerator LoadDatScene () {
		yield return new WaitForSeconds(0.3f);
		Application.LoadLevel(1);
	}
}
