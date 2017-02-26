using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoltenSand : Element {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool WaterInteract(EventTransferManager ETManager) {
		this.GetComponentInParent<Tile> ().GainElement (ElementType.Water);
		if (LevelManager.instance.TimeState == TimeStates.Past) {
			ETManager.OnLoseElement (this.GetComponentInParent<Tile> ().getTileID ());
		}
		return true;
	}

	public override bool FireInteract(EventTransferManager ETManager) {
		return false;
	}
}
