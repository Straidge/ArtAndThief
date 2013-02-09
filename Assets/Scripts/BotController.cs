using UnityEngine;
using System.Collections;

public class BotController : MonoBehaviour {

	public CharacterController controller;
	public float Nspeed;
	public float Aspeed;
	public Vector3 botDir;
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
		tr.LookAt(tr.position + botDir);
		//Aspeed = ;
	#else
		
	#endif
	
	}
	
	void FixedUpdate () 
		{
		controller.Move(tr.forward * Aspeed * 0.02f);
	}
}