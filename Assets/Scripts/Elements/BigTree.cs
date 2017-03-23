using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigTree : Element {

	public int tileID;

	public AudioSource audio;

	void Start () {
		elementType = ElementType.BigTree;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();
		this.audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
