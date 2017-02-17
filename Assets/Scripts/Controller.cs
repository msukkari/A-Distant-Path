using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public GameObject character;
	public CharacterController cc;
	public float speed = 5;

	public GameObject pivotPoint;

	// Use this for initialization
	void Start () {
		cc = GetComponent <CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		float camOrientation = pivotPoint.transform.rotation.eulerAngles.y;

		Vector3 forward = pivotPoint.transform.TransformDirection(Vector3.forward);
		forward.y = 0;
		forward = forward.normalized;
		Vector3 right  = new Vector3(forward.z, 0, -forward.x);


		float xDisp = Input.GetAxis ("LeftJoystickHorizontal") ;
		float zDisp = -1 * Input.GetAxis ("LeftJoystickVertical");


		Vector3 dispDir = (  xDisp * right + zDisp * forward);

		Vector3 orientation = Vector3.zero;
		orientation.x = Input.GetAxis ("RightJoystickVertical");
		orientation.z = Input.GetAxis ("RightJoystickHorizontal");

		if (orientation.sqrMagnitude >= 0.01) {
			transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg +camOrientation, transform.eulerAngles.z);
		}

		if (dispDir.sqrMagnitude > 0.4) {
			dispDir *= speed;
			cc.SimpleMove (dispDir);
		}


		if (Input.GetKey (KeyCode.W)) {
			cc.SimpleMove(new Vector3(speed, 0,0));
		}
		if (Input.GetKey (KeyCode.S)) {
			cc.SimpleMove(new Vector3(-speed, 0,0) );

		}
		if (Input.GetKey (KeyCode.D)) {
			cc.SimpleMove(new Vector3(0, 0,speed));
		}
		if (Input.GetKey (KeyCode.A)) {
			cc.SimpleMove(new Vector3(0, 0,-speed));
		}
	}
}
