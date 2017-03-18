using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {


	CharacterController controller;
	AudioSource audio;


	// Use this for initialization
	void Start () {
		
		this.controller = GetComponent<CharacterController>();
		this.audio = GetComponent<AudioSource>();

		// footsteps may only be used on the player..
		if (controller == null)
			Destroy(this);
	}


	private void playStep() {
		Debug.Log("Grounded: " + controller.isGrounded);
		Debug.Log("Velocity: " + controller.velocity.magnitude);
		Debug.Log("Is audio Playing: " + audio.isPlaying);

		if (controller.isGrounded == true && audio.isPlaying == false) {
				
			Debug.Log("step");
			audio.volume = Random.Range(0.8f, 1);
			audio.pitch = Random.Range(0.95f, 1.05f);

			audio.Play();
		}

	}

	// --- triggered by animation event ---
	public void footstepOne() { playStep(); }	
	public void footstepTwo() { playStep(); }
	
	// Update is called once per frame
	void Update () { }
}
