using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

	public float upSpeed = 10f;
	public float rightSpeed = 10f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, upSpeed * Time.deltaTime);
		transform.Rotate(Vector3.right, rightSpeed * Time.deltaTime);
	}
}
