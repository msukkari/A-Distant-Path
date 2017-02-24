using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Element {

	public int tileID;

	void Start () {
		elementType = ElementType.Ice;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
