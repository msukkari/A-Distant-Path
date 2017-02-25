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

	public override bool WaterInteract(EventTransferManager ETManager) {
		if (!this.GetComponent<MeshRenderer> ().enabled) {
			this.GetComponent<MeshRenderer> ().enabled = true;
			return true;
		} else {
			return false;
		}
	}

	public override bool FireInteract(EventTransferManager ETManager) {
		this.GetComponentInParent<Tile> ().GainElement (ElementType.Fire);
		return true;
	}

	/*private void CreateOrDestroyIceBlock(bool create) {
		Element[] stacksOfIce = this.GetComponentInParent<Tile> ().GetComponentsInChildren<Ice> ();

		if (create) {
			if (this.GetComponent<MeshRenderer> ().enabled == false) {
				this.GetComponent<MeshRenderer> ().enabled = true;
			}
		} else {
			if (stacksOfIce.Length == 1) {
				this.GetComponentInParent<Tile> ().GainElement (ElementType.Fire);
			}
			Destroy (stacksOfIce [stacksOfIce.Length - 1].gameObject);
		}
	}*/
}
