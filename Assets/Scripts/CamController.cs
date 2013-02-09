using UnityEngine;
using System.Collections;

public class CamController : MonoBehaviour {
	
	private Transform tr;
	public Transform cible;
	public Vector2 minMaxX;
	public Vector2 minMaxZ;
	public float velocityX;
	public float velocityZ;
	
	public float offsetZ;

	// Use this for initialization
	void Start () {
		cible = GameObject.Find("Player - Voleur").transform;
		tr = transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (cible != null)
			tr.position = new Vector3(Mathf.Clamp(Mathf.SmoothDamp(tr.position.x, cible.position.x, ref velocityX, 0.2f), minMaxX.x, minMaxX.y), tr.position.y, Mathf.Clamp(Mathf.SmoothDamp(tr.position.z, cible.position.z - offsetZ, ref velocityZ, 0.2f), minMaxZ.x, minMaxZ.y));
	}
}
