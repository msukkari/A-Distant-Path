using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetachCamera : MonoBehaviour {

	private Player player;
	
	private CameraControls camControl;

	// Use this for initialization
	void Start () {
		this.camControl = Camera.main.GetComponent<CameraControls>();
	}
	
	void trigger() {
		this.camControl.setCharacter(null);
	}

	public void OnTriggerEnter(Collider other){
		Player player = other.gameObject.GetComponent<Player>();

		// check if entered gameobject is the player
		if(player != null){
			this.player = player;
			trigger();
		}
	}


	// Update is called once per frame
	void Update () {
		Destroy(this.camControl);
	}
}
