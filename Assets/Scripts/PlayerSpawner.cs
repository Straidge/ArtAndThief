using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {
	
	public GameObject player;

	// Use this for initialization
	void Start () {
		Instantiate(player, transform.position, transform.rotation);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
