using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour{

	public Gun[] guns;
	public Tile currentLocation;

	public Dictionary<ElementType, int> elementsInventory = new Dictionary<ElementType, int>();

	private float elementTimeCount = -1f;
	private const int PICKUP_TIME_THRESHOLD = 1;

	private int currentGun = 0;

	void Start() {
		guns = GetComponentsInChildren<Gun> ();
	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.E))
			printNTiles();
		
		CheckElementPickup();

		if (Input.GetKeyDown (KeyCode.Q)) {
			ChangeGun ();
		}

		if (Input.GetKeyDown (KeyCode.E)) {
			guns [currentGun].ChangeMode ();
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Debug.Log ("Started charge");
			chargingWeapon = true;
		}

		if (chargingWeapon) {
			Debug.Log ("Charging weapon");
			currentCharge += Time.deltaTime;
		}

		if (Input.GetKeyUp (KeyCode.Space)) {
			Debug.Log ("Released at charge = " + currentCharge);

			if (currentCharge > 1.0f) {
				guns [currentGun].AreaShot ();
			}
			currentCharge = 0.0f;
			chargingWeapon = false;
		}
	}

	#region Gun Methods

	public void Shoot() {
		RaycastHit hitInformation;

		int layer = 1 << 8;
		
		// Commented this out as per request to have no mouse input methods.
		// However the idea here is that you apply the player's currently active gun's
		// Shoot() method to whatever tile you select by this raycast.
		// So if your ElementAbsorber gun is currently active, it would suck that tile's element
		// and increase the player's owned elements in inventory.
		// If you had ElementShooter active, it would shoot your currently selected element
		// at the tile you targeted with the mouse. So if you targeted a water tile with fire
		// element currently active in your gun, it would turn the tile to a steam tile
		// If the tile had no element, it would turn it to a fire tile etc.

		/*bool hit = Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInformation, Mathf.Infinity, layer);

		if (hit) {
			if (hitInformation.transform.gameObject.tag == "Tile") {
				Tile tileShot = hitInformation.transform.gameObject.GetComponent<Tile> ();

				//Debug.Log ("Current Tile: " + currentLocation.name + ", position = " + currentLocation.transform.position);
				//Debug.Log ("Tile Shot: " + tileShot.name + ", position = " + tileShot.transform.position);

				// Limited range to 2 neighbors for now. Can be adjusted later to accomodate
				foreach (Tile neighbor in currentLocation.neighbors) {
					if (neighbor.Equals (tileShot) || neighbor.neighbors.Contains(tileShot)) {
						guns[currentGun].ShootGun (tileShot, guns[currentGun].chargesPerHit);
					}
				}

			} else {
				Debug.Log (hitInformation.transform.gameObject.name);
			}
		}*/
	}

	public void ChangeGun() {
		currentGun = (currentGun + 1) % guns.Length;
		//Debug.Log ("Changed to gun at index: " + currentGun);
		Debug.Log("Changed current gun to: " + guns[currentGun].name);
	}

	#endregion

	#region Element Methods

	public bool HasElement(ElementType element, int number) {
		return elementsInventory.ContainsKey (element) && elementsInventory [element] >= number;
	}

	public void GainElement(ElementType element, int number) {
		if (elementsInventory.ContainsKey (element)) {
			elementsInventory [element] += number;
		} else {
			elementsInventory.Add (element, number);
		}
	}

	public void LoseElement(ElementType element, int number) {
		if (elementsInventory.ContainsKey (element) && elementsInventory [element] >= number) {
			elementsInventory [element] -= number;
		}
	}

	private void CheckElementPickup() {

		// check if player is attempting to collect water
		if (!Input.GetKey(KeyCode.F)) {
			elementTimeCount = 0;
			return;
		}

		elementTimeCount += Time.deltaTime;

		if (elementTimeCount >= PICKUP_TIME_THRESHOLD) {
			Debug.Log("element pickup");
			printNTiles();
			elementTimeCount = 0;

			if(next2Resource()){
				Debug.Log("PLAYER IS NEXT TO A RESOURCE");
			}
		}
	}

	private bool next2Resource(){
		Tile curTile = getCurTile();
		List<int> nList = curTile.getNTileIDList();

		foreach(int id in nList){
			Tile neighbor = LevelManager.instance.getTileAt(id);
			// Changed from: if(neighbor.state == Tile.State.Water || neighbor.state == Tile.State.Fire) because 
			// we'll have more than 10-15 element types soon. would be painful to write out all of them.
			// That's why I use parent classes
			if(neighbor.state == Tile.State.Water || neighbor.state == Tile.State.Fire){
				return true;
			}

		}

		return false;
	}

	#endregion
	
	/* 
	// OLD METHOD
	public int getCurTileID(){
		int x = (int)Math.Round((transform.position.x), MidpointRounding.AwayFromZero) * GameManager.GRID_SIZE;
		int z = (int)Math.Round(transform.position.z, MidpointRounding.AwayFromZero);
		Debug.Log("X CORD: " + x);
		Debug.Log("Z CORD: " + z);

		return x + z;
	}
	*/

	#region Tile ID Methods

	public int getCurTileID(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if(cur != null)
				return cur.tileID;
		}

		return -1;
	}

	public Tile getCurTile(){
		return LevelManager.instance.getTileAt(this.getCurTileID());
	}

	// Using this to DEBUG
	public void printNTiles(){

		int curTileID = getCurTileID();
		Tile curTile = LevelManager.instance.getTileAt(curTileID);
		if(curTile == null){
			Debug.Log("TILE NOT FOUND");
			return;
		}
		List<int> neighborList = curTile.getNTileIDList();


		foreach(int n in neighborList)
			Debug.Log(n);

	}

	#endregion

}
