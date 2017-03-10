using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalCube : Element {

	public int tileID;

	void Start () {
		elementType = ElementType.MetalCube;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();
		this.climable = true;
	}

	// Update is called once per frame
	void Update () {
		
	}

	public override bool WaterInteract(EventTransferManager ETManager) {



		if(ETManager != null && LevelManager.instance.TimeState == TimeStates.Past) {
			ETManager.OnMetalRust (this.GetComponentInParent<Tile> ().getTileID ());
		}
		this.GetComponentInParent<Tile> ().GainElement (ElementType.Water);
		return true;
	}

	public override bool FireInteract(EventTransferManager ETManager) {
		return false;
	}
}
