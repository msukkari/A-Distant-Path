using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sand : Element {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool WaterInteract(EventTransferManager ETManager) {
		return false;
	}

	public override bool FireInteract(EventTransferManager ETManager) {
		this.GetComponentInParent<Tile> ().GainElement (ElementType.Fire);
		if (LevelManager.instance.TimeState == TimeStates.Past) {
			ETManager.OnSandFire (this.GetComponentInParent<Tile> ().getTileID ());
		}
		return true;
	}
}
