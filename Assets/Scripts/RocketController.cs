using UnityEngine;
using System.Collections;

public class RocketController : MonoBehaviour {
	
	
	public float speed;
	public Transform tr;
	public GameObject boom;
	public AudioClip kaBoom;
	public AudioClip kaBoomAlt;

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
			other.GetComponent<PlayerController>().Lose();
		}
		if (Random.value > 0.5f)
			audio.PlayOneShot(kaBoom);
		else
			audio.PlayOneShot(kaBoomAlt);
		Instantiate(boom, transform.position, transform.rotation);
		Destroy(gameObject);
	}
}
