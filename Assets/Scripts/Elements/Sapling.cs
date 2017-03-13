using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sapling : Element {

	public int tileID;

	void Start () {
		elementType = ElementType.Sapling;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool FireInteract(EventTransferManager ETManager) {
	if (LevelManager.instance.TimeState == TimeStates.Past) {
		ETManager.OnSaplingFire (this.GetComponentInParent<Tile>().gameObject.transform.position);
	}

	this.GetComponentInParent<Tile> ().GainElement (ElementType.Fire);
	return true;
	}
}
