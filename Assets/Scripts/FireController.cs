using UnityEngine;
using System.Collections;

public class FireController : MonoBehaviour {
	
	public GameObject rocket;
	public Transform gun;
	public float tCD;
	private float t;
	
	void Update () {
		t += Time.deltaTime;
	}

	public void Fire () {
		if (t > tCD) {
			Instantiate(rocket, gun.position, gun.rotation);
			t = 0;
		}
	}
}
