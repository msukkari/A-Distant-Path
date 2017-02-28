using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Element {

	public bool destroyTileOnLose = true;

	// Use this for initialization
	void Start () {
		elementType = ElementType.Water;
		navigatable = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool FireInteract(EventTransferManager ETManager) {
		return this.GetComponentInParent<Tile> ().GainElement (ElementType.Fire);;
	}
}
