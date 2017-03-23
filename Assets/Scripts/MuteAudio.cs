using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void OnTriggerEnter(Collider other){
		Debug.Log("TRIGGER ON CHECKPOINT");

		Player player = other.gameObject.GetComponent<Player>();

		if(player != null){
			AudioManager.instance.primary.Stop();	
			AudioManager.instance.stop = true;	
		}

	}
}
