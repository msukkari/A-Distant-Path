using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCube : Element {

	public int tileID;

	void Start () {
		elementType = ElementType.MetalCube;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
