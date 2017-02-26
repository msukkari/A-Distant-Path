using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Element {

	public int tileID;

	public GameObject iceCubePrefab;
	public List<GameObject> iceCubes = new List<GameObject> ();

	void Start () {
		elementType = ElementType.Ice;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public override bool WaterInteract(EventTransferManager ETManager) {
		/*if (!this.GetComponent<MeshRenderer> ().enabled) {
			this.GetComponent<MeshRenderer> ().enabled = true;
			return true;
		} else {
			return false;
		}*/
		CreateIceCube ();
		return true;
	}

	public override bool FireInteract(EventTransferManager ETManager) {
		//this.GetComponentInParent<Tile> ().GainElement (ElementType.Fire);
		DestroyIceCube();
		return true;
	}

	private void CreateIceCube() {
		GameObject iceCube = Instantiate (iceCubePrefab, this.GetComponentInParent<Tile> ().transform.position + new Vector3(0.0f, (float)(iceCubes.Count + 1) ,0.0f), Quaternion.identity, this.transform);
		iceCubes.Add (iceCube);
	}

	private void DestroyIceCube() {
		if (iceCubes.Count > 0) {
			GameObject lastCube = iceCubes [iceCubes.Count - 1];
			iceCubes.Remove (lastCube);
			Destroy (lastCube);
		} else {
			this.GetComponentInParent<Tile> ().SetNavigatable (false);
			this.GetComponentInParent<Tile> ().GetComponent<MeshRenderer> ().enabled = false;
		}
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
