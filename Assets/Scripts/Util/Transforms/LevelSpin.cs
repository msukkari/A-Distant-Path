using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSpin : MonoBehaviour {

	public float upSpeed = 10f;
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(this.transform.up, upSpeed * Time.deltaTime);
	}
}
