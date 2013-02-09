using UnityEngine;
using System.Collections;

public class ItemToSteal : MonoBehaviour {
	
	public float valeur;
	public bool caught;
	
	void Update () {
		if (caught)
			renderer.enabled = false;
	}
	
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player" && !caught) {
			other.GetComponent<PlayerController>().nearObject = true;
			other.GetComponent<PlayerController>().item = gameObject;
		}
	}
	
	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			other.GetComponent<PlayerController>().nearObject = false;
		}
	}

}
