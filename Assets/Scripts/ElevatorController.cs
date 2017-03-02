using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour {
	public float speed = 1;
	public Vector3 startPos;
	public Vector3 endPos;
	public GameObject parent;

	private bool isTriggered = false;
	// Use this for initialization
	void Start () {
		startPos = parent.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isTriggered & parent.transform.position.y < endPos.y) {
			parent.transform.position += Vector3.up * speed * 0.01f;
		} else if ((!isTriggered) & parent.transform.position.y > startPos.y){
			parent.transform.position += Vector3.down * speed * 0.01f;
		}
	}


	void OnTriggerEnter(Collider other) {
		
		if (other.tag == "Player") {
			isTriggered = true;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			isTriggered = false;
		}
	}


	
}
