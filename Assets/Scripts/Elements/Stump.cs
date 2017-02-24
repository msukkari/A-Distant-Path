using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stump : Element {

	public int tileID;

	void Start () {
		elementType = ElementType.Stump;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
