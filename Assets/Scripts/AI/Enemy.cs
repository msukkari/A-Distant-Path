﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	public GameObject behavior;


	// Use this for initialization
	void Start () {

		GameObject behaviorGO = Instantiate (behavior) as GameObject;
		behaviorGO.transform.parent = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Tile getCurTile(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if(cur != null)
				return cur;
		}

		return null;
	}

	public int getCurTileID(){
		return this.getCurTile() == null ? -1 : this.getCurTile().id; 
	}

	
}