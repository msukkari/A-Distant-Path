using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public GameObject character;
	public Rigidbody rb;
	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.W)) {
			rb.AddForce (new Vector3 (0, 0, 1));
		}
		if (Input.GetKey (KeyCode.S)) {
			rb.AddForce (new Vector3 (0, 0, -1));
		}
		if (Input.GetKey (KeyCode.D)) {
			rb.AddForce (new Vector3 (1, 0, 0));
		}
		if (Input.GetKey (KeyCode.A)) {
			rb.AddForce (new Vector3 (-1, 0, 0));
		}
	}
}
