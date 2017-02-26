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

	public override bool WaterInteract(EventTransferManager ETManager) {
		if (LevelManager.instance.TimeState == TimeStates.Past) {
			ETManager.OnStumpWater (this.GetComponentInParent<Tile> ().getTileID ());
		}
		this.GetComponentInParent<Tile> ().GainElement (ElementType.Water);
		return true;
	}

	public override bool FireInteract(EventTransferManager ETManager) {
		return false;
	}
}
