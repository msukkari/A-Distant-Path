using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraController : MonoBehaviour {
	public Camera cam;

	public float zoomSpeed = 2.0f;
	private Vector3 zoomVelocity = Vector3.zero; 
	public float smoothTime = 1.0f;
	public float maxZoom = 10.0f;
	public float minZoom = 1.0f;

	public GameObject character;
	float dragFactor = 0.1f;

	public float rotateSpeed;

	// Use this for initialization
	void Start () {
		cam = GetComponentInChildren<Camera> ();
	}

	private float x = 0;
	private float y = 0;

	// Update is called once per frame
	void Update () {
		Vector3 turnDir = Vector3.zero;
		turnDir.x = Input.GetAxis ("RightJoystickHorizontal");
		turnDir.y = Input.GetAxis ("RightJoystickVertical");

		if (turnDir.sqrMagnitude >= 0.1) {
			x += turnDir.x;
			if (turnDir.y >= 0.4 || turnDir.y <= -0.4) {
				y += turnDir.y;
			}
			y = Mathf.Clamp (y, 10, 70);
			transform.rotation = Quaternion.Euler (new Vector3 (y, x, 0));
		}
		Vector3 distance = character.transform.position - transform.position;
		if (distance.sqrMagnitude > 0) {
			if (distance.sqrMagnitude < 0.01) {
				transform.position = character.transform.position;
			} else {
				transform.Translate(distance * dragFactor);
			}

		}
	}
}
