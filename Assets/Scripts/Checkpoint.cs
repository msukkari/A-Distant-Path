using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {

	public Vector3 SpawnPoint;
	// Use this for initialization
	void Start () {
		this.SpawnPoint = new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other){
		Debug.Log("TRIGGER ON CATCH");

		Player player = other.gameObject.GetComponent<Player>();

		if(player != null){
			player.curRespawnPoint = this.SpawnPoint;
		}
	}
}
