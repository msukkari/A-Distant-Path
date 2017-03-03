using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bucket : MonoBehaviour {

	public Tile tileLocation;
	public Element heldElement;

	public int maxQuantity = 1;
	public int currentQuantity;

	public bool isPickedUp;

	private Tile lastValidTile;

	public GameObject fireSpherePrefab;
	public GameObject waterSpherePrefab;

	// Use this for initialization
	void Start () {
		currentQuantity = 0;
		isPickedUp = false;
		tileLocation = getCurTile ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public Tile getCurTile(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if (cur != null)
				lastValidTile = cur;
			return cur;
		}

		return lastValidTile;
	}

	public int getCurTileID(){
		return this.getCurTile() == null ? lastValidTile.id : this.getCurTile().id; 
	}

	public void GetHitByElement(ElementType elementType) {
		if (!isPickedUp && IsValidElement(elementType) && currentQuantity < maxQuantity) {
			switch (elementType) {
			case ElementType.Fire:
				//DISPLAY FIRE PREFAB ON IT
				if (heldElement == null) {
					GameObject fireGO = Instantiate (fireSpherePrefab, this.transform);
					this.heldElement = fireGO.GetComponent<Fire> ();
					fireGO.transform.position = this.transform.position;
					fireGO.transform.localPosition = Vector3.up * 0.4f;
					Debug.Log ("BUCKET HAS FIRE NOW");
				}
				currentQuantity++;
				break;
			case ElementType.Water:
				if (heldElement == null) {
					GameObject waterGO = Instantiate (waterSpherePrefab, this.transform);
					this.heldElement = waterGO.GetComponent<Water> ();
					waterGO.transform.position = this.transform.position;
					waterGO.transform.localPosition = Vector3.up * 0.4f;
					Debug.Log ("BUCKET HAS Water NOW");
				}
				currentQuantity++;
				break;
			default:
				break;
			}
		}
	}

	public bool IsValidElement(ElementType elementType) {
		return this.heldElement == null || this.heldElement.elementType == elementType;
	}

	public Element LoseElement() {
		Element returnElement = this.heldElement;
		currentQuantity = 0;
		Destroy (this.heldElement.gameObject);
		this.heldElement = null;
		return returnElement;
	}
}
