using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {
	
	public GameObject rocket;
	public Transform gun;
	public float tCD;
	private float t;
	
	public AudioClip fireRocketA;
	public AudioClip fireRocketB;
	
	void Update () {
		t += Time.deltaTime;
	}

	public void Fire () {
		if (t > tCD) {
			transform.rotation *= Quaternion.AngleAxis(Random.Range(-20, 20), Vector3.up);
			Instantiate(rocket, gun.position, gun.rotation);
			t = 0;
			if (Random.value > 0.5f)
				audio.PlayOneShot(fireRocketA);
			else
				audio.PlayOneShot(fireRocketB);
				
		}
	}
}
