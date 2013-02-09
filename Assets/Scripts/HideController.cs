using UnityEngine;
using System.Collections;

public class HideController : MonoBehaviour {
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			other.GetComponent<PlayerController>().hide = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			other.GetComponent<PlayerController>().hide = false;
		}
	}
}
