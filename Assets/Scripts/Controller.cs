using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {
	public GameObject character;
	public CharacterController cc;
	public float speed = 5;


	// Use this for initialization
	void Start () {
		cc = GetComponent <CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 dispDir = Vector3.zero;
		dispDir.x = Input.GetAxis ("LeftJoystickHorizontal");
		dispDir.z = Input.GetAxis ("LeftJoystickVertical");

		if (dispDir.sqrMagnitude > 0.4) {
			dispDir *= speed;
			cc.SimpleMove (dispDir);
		}

		if (Input.GetKey (KeyCode.W)) {
			cc.SimpleMove(new Vector3(speed, 0,0));
		}
		if (Input.GetKey (KeyCode.S)) {
			cc.SimpleMove(new Vector3(-speed, 0,0));

		}
		if (Input.GetKey (KeyCode.D)) {
			cc.SimpleMove(new Vector3(0, 0,speed));
		}
		if (Input.GetKey (KeyCode.A)) {
			cc.SimpleMove(new Vector3(0, 0,-speed));
		}
	}
}
