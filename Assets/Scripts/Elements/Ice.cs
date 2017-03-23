using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : Element {

	public int tileID;
    public GameObject player;

	public GameObject iceCubePrefab;
    //public GameObject icePrefab;
    //public List<GameObject> iceCubes = new List<GameObject> ();

	void Start () {
		elementType = ElementType.Ice;
		this.tileID = transform.parent.GetComponent<Tile>().getTileID();
        this.player = GameObject.FindGameObjectWithTag("Player");
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
		Level level = GameObject.FindGameObjectWithTag ("Level").GetComponent<Level> ();

		if (level != null) {
			Tile topTile = this.GetComponentInParent<Tile> ().GetTopTile ();
			GameObject newIceCubeTile = Instantiate (iceCubePrefab, topTile.transform.position + new Vector3(0.0f, topTile.tileScale.y ,0.0f), Quaternion.identity, GameObject.FindGameObjectWithTag("Level").transform) as GameObject;
			newIceCubeTile.tag = "Tile";
			newIceCubeTile.GetComponent<Tile> ().isGroundTile = false;
            newIceCubeTile.GetComponent<Tile>().EventManager = topTile.EventManager;

            GameObject newIceElement = Instantiate (this.gameObject, newIceCubeTile.transform);
			// Should probably assign a unique id to the created tile as well!
			LevelManager.instance.AddTileToList (newIceCubeTile.GetComponent<Tile> ());
		}
	}

	private void DestroyIceCube() {
		Tile topTile = this.GetComponentInParent<Tile> ().GetTopTile ();
		//if (topTile.isGroundTile) {
		//	topTile.SetNavigatable (true);
        //    Destroy(topTile.gameObject);
		//} else {
		//	topTile.gameObject.SetActive (false);
		//}
        RaycastHit hit;
        Ray ray = new Ray(topTile.transform.position, Vector3.down);
        bool isTile = false;
        if (Physics.Raycast(ray, out hit)) {
            Component[] Components = hit.collider.GetComponentsInParent<Component>();
            for(int i=0;i< Components.GetLength(0);i++) {
                Component current = Components[i];
                if (current.tag == "Tile") {
                   Tile hitTile = hit.collider.GetComponentInParent<Tile>();
                   isTile = true;
                   if (hitTile.element != null){
                        if (hitTile.element.elementType != ElementType.Ice)
                            {
                                hitTile.element.WaterInteract(hitTile.EventManager);
                            }
                        }
                        else
                        {
                            hitTile.GainElement(ElementType.Water);
                        }
                    i = Components.GetLength(0);
                    }
                }
            }
        if (isTile)
        {
            topTile.gameObject.SetActive(false);
        } else
        {
            Player playerScript = player.GetComponent<Player>();
            playerScript.GainElement(ElementType.Fire,1);
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
