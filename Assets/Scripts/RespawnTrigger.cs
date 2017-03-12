using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnTrigger : MonoBehaviour {

	private Vector3 playerInitialPosition;

	// Use this for initialization
	void Start () {
		playerInitialPosition = GameObject.FindGameObjectWithTag ("Player").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other) {
		other.gameObject.transform.position = playerInitialPosition;
	}
}
