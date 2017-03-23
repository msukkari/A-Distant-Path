using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catch : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	public void OnTriggerEnter(Collider other){
		//Debug.Log("TRIGGER ON CATCH");

		Player player = other.gameObject.GetComponent<Player>();
		PlayerControls controls = other.gameObject.GetComponent<PlayerControls>();

		if(player != null){
			controls.verticalVelocity = 0;
			player.Respawn();
		}
	}
}
