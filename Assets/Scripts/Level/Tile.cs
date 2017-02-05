using UnityEngine;
using System;
using System.Collections;

public class Tile : MonoBehaviour {

	public int tileID;

	private Level level;

	// Use this for initialization
	void Start(){
		level = transform.parent.GetComponent<Level>();

		Vector3 tilePos = transform.position;
		tileID = (int)(tilePos.x  * 10) + (int)tilePos.z;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getTileID(){return tileID;}


}