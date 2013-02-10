using UnityEngine;
using System.Collections;

public class SoundController : MonoBehaviour {
	
	public AudioClip audioclip;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
		if (audioclip != audio.clip) {
			audio.volume = Mathf.Max(0, audio.volume - Time.deltaTime * 2f);
			if (audio.volume == 0) {
				audio.clip = audioclip;
				audio.Play();
				}
		}
		else {
			audio.volume = Mathf.Min(1, audio.volume + Time.deltaTime * 2f);
		}
	
	}
	
	public void SetMusic (AudioClip music) {
		audio.clip = music;
		audioclip = music;
		audio.Play();
	}
	
	public void SetSound (AudioClip sound) {
		Debug.Log(sound);

	}
}
