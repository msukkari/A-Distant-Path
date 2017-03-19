using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {
	public float speed = 1;
	public Vector3 startPos;
	public Vector3 endPos;
	public GameObject parent;

	private bool isTriggered = false;

	// indicates direction change
	private bool hasChanged = false;

	private AudioSource audio;

	// Use this for initialization
	void Start () {
		startPos = parent.transform.position;
		this.audio = parent.transform.GetComponent<AudioSource>();
		this.audio.volume = 0.7f;
	}
	
	// Update is called once per frame
	void Update () {

		bool atTop = parent.transform.position.y >= endPos.y;
		bool atBottom = parent.transform.position.y <= startPos.y;

		if (hasChanged) {
			audio.Stop();
			hasChanged = false;
		}

		if (isTriggered & !atTop) {
			parent.transform.position += Vector3.up * speed * 0.01f;


			if(!audio.isPlaying) {
				audio.Play();
			} 

		} else if ((!isTriggered) & !atBottom){
			parent.transform.position += Vector3.down * speed * 0.01f;

			if(!audio.isPlaying) {
				audio.Play();
			}
		}

		if (atTop || atBottom)
			audio.Stop();
	}


	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Player") {
			isTriggered = true;
			hasChanged = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			isTriggered = false;
			hasChanged = true;
		}
	}


	
}
