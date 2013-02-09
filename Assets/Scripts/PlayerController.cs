using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	
	public CharacterController controller;
	public float Nspeed;
	private float Aspeed;
	public Vector3 dir;
	private Transform tr;


	void Start () 
		{
		controller = GetComponent<CharacterController>();
		tr = GetComponent<Transform>();
	}
	
	// Update is called once per frame
	void Update () 
		{
	#if UNITY_EDITOR
		dir = new Vector3 (Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		tr.LookAt(tr.position + dir);
		Aspeed = Mathf.Min(Mathf.Abs(Input.GetAxisRaw("Horizontal")) + Mathf.Abs(Input.GetAxisRaw("Vertical")), 1) * Nspeed;
	#else
		
	#endif
	
	}
	
	void FixedUpdate () 
		{
		controller.Move(tr.forward * Aspeed * 0.02f);
	}
}
