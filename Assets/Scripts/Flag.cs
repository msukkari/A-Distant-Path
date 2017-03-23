using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour {

	public Vector3 endPos;

	public bool isTriggered = false;

	// Use this for initialization
	void Start () {
		endPos = this.transform.position + new Vector3(0f, 2.5f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		bool atTop = this.transform.position.y >= endPos.y;

		if (isTriggered & !atTop) {
			this.transform.position += Vector3.up * Time.deltaTime * 2;
		}
	}
}
