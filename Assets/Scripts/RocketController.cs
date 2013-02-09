using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	
	
	public float speed;
	public Transform tr;

	void Start () {
		tr = transform;
		Destroy(gameObject, 5);
	}
	
	void FixedUpdate () {
		transform.position += transform.forward * 0.02f * speed;
	}
	
	void OnTriggerEnter(Collider other) {
		if (other.tag == "butin") {
			other.GetComponent<ItemToSteal>().caught = true;
		}
		if (other.tag == "Player") {
			Application.LoadLevel(0);
		}
		Destroy(gameObject);
	}
}
