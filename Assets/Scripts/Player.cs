using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour{

	public Gun[] guns;
	
	// current tile location of player (used for AI)
	public Tile currentLocation;

	public Dictionary<ElementType, int> elementsInventory = new Dictionary<ElementType, int>();

	private float elementTimeCount = -1f;
	private const int PICKUP_TIME_THRESHOLD = 1;

	private int currentGun = 0;
	private bool chargingWeapon = false;
	private float currentCharge = 0.0f;

	private Tile lastValidTile;
	private bool frozen;
	private float frozenSinceSeconds = 0.0f;
	public const float unfreezeSeconds = 2.0f;

	// Get AIManager instance
	private AIManager am = AIManager.instance;
  
	// This is linked when the player is initiated in LevelManager
	public EventTransferManager ETmanager;

	void Start() {
		guns = GetComponentsInChildren<Gun> ();

		elementsInventory[ElementType.Fire] = 10;
		elementsInventory[ElementType.Water] = 10;


		currentLocation = getCurTile();
    
		// Sanity check
		foreach(Gun gun in guns){
			gun.owner = this;
		}

		lastValidTile = getCurTile ();
	}

	void Update(){
		if (!frozen) {
			if (Input.GetKeyDown (KeyCode.E))
				printNTiles ();

			CheckElementPickup ();

			/*
		if (Input.GetKeyDown (KeyCode.Q)) {
			ChangeGun ();
		}
		*/

			if (Input.GetKeyDown (KeyCode.E)) {
				guns [currentGun].ChangeMode ();
			}

			// Suck tile only if space is pressed and the player is not moving
			if ((Input.GetKey (KeyCode.Space) || Input.GetButton ("XButton")) && NotMoving ()) {
				chargingWeapon = true;
			}

			// R Shoots water in the front tile (for now)
			if (Input.GetKeyDown (KeyCode.R)) {
				ShootWaterInFrontTile ();
			}

			// Q Shoots fire in the front tile (for now)
			if (Input.GetKeyDown (KeyCode.Q)) {
				ShootFireInFrontTile ();	
			}

			if (chargingWeapon && NotMoving ()) {
				currentCharge += Time.deltaTime;

				if (currentCharge > 1.0f) {
					guns [currentGun].AreaShot ();
					currentCharge = 0.0f; // Charge is reset after the areashot to insure that an areashot is only done once per second
					chargingWeapon = false;
				}
			} else {
				currentCharge = 0.0f;
				chargingWeapon = false;
			}

			if (Input.GetKeyUp (KeyCode.Space)) {
				currentCharge = 0.0f;
				chargingWeapon = false;

			}

			// handle AI trigger
			AITrigger ();
		} else {
			frozenSinceSeconds += Time.deltaTime;

			// Player would either freeze itself, or enemy would externally unfreeze the player once reaches its initial location
			if (frozenSinceSeconds >= unfreezeSeconds) {
				//Freeze(false)
			}

		}

	}

	public void Freeze(bool freeze) {
		MonoBehaviour[] allScripts = this.gameObject.GetComponents<MonoBehaviour> ();

		if (freeze) {
			this.frozen = true;
			Debug.Log ("Player is frozen!!!");
			foreach (MonoBehaviour script in allScripts) {
				if (script != this) {
					script.enabled = false;
				}
			}
		} else {
			foreach (MonoBehaviour script in allScripts) {
				script.enabled = true;
			}
			this.frozen = false;
			frozenSinceSeconds = 0.0f;
			Debug.Log ("PLAYER UNFROZEN!");
		}

	}			

	private void AITrigger() {

		if (playerOnNewTile()) {
			am.AIStateEvent(AIEvents.PlayerOnNewTile);
		}

	}

	// playerOnNewTile: returns true if the player is on a new tile
	private bool playerOnNewTile() {

		// get current tile player is on
		Tile temp = getCurTile();

		// if the player has moved
		if (temp != currentLocation) {
			currentLocation = temp;
			return true;
		}

		return false; 
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

	private void ShootWaterInFrontTile() {
		if (HasElement (ElementType.Water, 1)) {
			Tile front = getFrontTile();

			if(this.elementsInventory[ElementType.Water] > 0 && front != null){
				if (front.element != null) {
					if (front.element.WaterInteract (ETmanager)) {
						this.elementsInventory[ElementType.Water]--;
					}
				} else {
					if(front.GainElement (ElementType.Water)){
						this.elementsInventory[ElementType.Water]--;
					}
				}
			}

			foreach (Enemy enemy in AIManager.instance.enemies) {
				if (enemy.getCurTile () == front) {
					enemy.GetHitByElement (ElementType.Water);
				}
			}
		}
	}

	private void ShootFireInFrontTile() {
		if (HasElement (ElementType.Fire, 1)) {
			Tile front = getFrontTile ();

			if (this.elementsInventory [ElementType.Fire] > 0 && front != null) {
				if (front.element != null) {
					if (front.element.FireInteract (ETmanager)) {
						this.elementsInventory [ElementType.Fire]--;
					}
				} else {
					if(front.GainElement (ElementType.Fire))
						this.elementsInventory [ElementType.Fire]--;
				}
			}

			foreach (Enemy enemy in AIManager.instance.enemies) {
				if (enemy.getCurTile () == front) {
					enemy.GetHitByElement (ElementType.Fire);
				}
			}
		}
	}

	private void ShootWaterOnTile(Tile tile) {
		if (HasElement (ElementType.Water, 1)) {
			if(tile == null){
				Debug.Log("ERROR FINDING TILE in ShootWaterOnTile");
				return;
			}

			if (tile.element != null) {
				if (tile.element.WaterInteract (ETmanager)) {
					this.elementsInventory[ElementType.Water]--;
				}
			} else {
				if(tile.GainElement (ElementType.Water)){
					this.elementsInventory[ElementType.Water]--;
				}
			}

			foreach (Enemy enemy in AIManager.instance.enemies) {
				if (enemy.getCurTile () == tile) {
					enemy.GetHitByElement (ElementType.Water);
				}
			}
		}
	}

	private void ShootFireOnTile(Tile tile) {
		if (HasElement (ElementType.Fire, 1)) {
			if(tile == null){
				Debug.Log("ERROR FINDING TILE in ShootFireOnTile");
				return;
			}

			if (tile.element != null) {
				if (tile.element.FireInteract (ETmanager)) {
					this.elementsInventory[ElementType.Fire]--;
				}
			} else {
				if(tile.GainElement (ElementType.Fire)){
					this.elementsInventory[ElementType.Fire]--;
				}
			}

			foreach (Enemy enemy in AIManager.instance.enemies) {
				if (enemy.getCurTile () == tile) {
					enemy.GetHitByElement (ElementType.Fire);
				}
			}
		}
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

		foreach(Tile neighbor in curTile.neighbors){
			// Changed from: if(neighbor.state == Tile.State.Water || neighbor.state == Tile.State.Fire) because 
			// we'll have more than 10-15 element types soon. would be painful to write out all of them.
			// That's why I use parent classes
			if(neighbor.element != null){
				return true;
			}

		}

		return false;
	}

	private bool NotMoving() {
		return (Mathf.Abs(Input.GetAxis ("Horizontal")) < 0.1f) && (Mathf.Abs(Input.GetAxis ("Vertical")) < 0.1f);
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

	public Tile getCurTile(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if (cur != null){
				lastValidTile = cur;
				return cur;
			}
		}

		return lastValidTile;
	}

	public int getCurTileAt(int tileID){
		return this.getCurTile() == null ? lastValidTile.id : this.getCurTile().id; 
	}

	public Tile getFrontTile(){
		RaycastHit hit;

		Vector3 direction = Quaternion.AngleAxis(20, this.transform.forward) * this.transform.forward;
		Debug.DrawRay(transform.position, direction, Color.red, 1, false);

		if(Physics.Raycast(transform.position, direction, out hit)){
			
			if(hit.collider.tag == "Tile"){
				Tile cur = hit.collider.gameObject.GetComponent<Tile>();

				if(cur != null && cur.enabled){
					Debug.Log(cur.getTileID());
					return cur;
				}
			}
			else {
				Tile cur = hit.collider.gameObject.GetComponentInParent<Tile>();

				if(cur != null && cur.enabled){
					Debug.Log(cur.getTileID());
					return cur;
				}
			}
		}

		Debug.Log("TILE NOT FOUND");
		return null;
	}

	// Using this to DEBUG
	public void printNTiles(){

		Tile curTile = this.getCurTile();
		if(curTile == null){
			Debug.Log("TILE NOT FOUND");
			return;
		}

		foreach(Tile n in curTile.neighbors)
			Debug.Log(n.getTileID());

	}

	#endregion

    public void throwMaterial(Tile tile) {
        Debug.Log("Throw material!");
        if(tile != null){
        	this.ShootWaterOnTile(tile);
        }
    }

    public void placeWaypoint(Vector3 position) {
        Debug.Log("Place waypoint");
    }

    public void interactInFront(Tile tile) {
        Debug.Log("Interact in front!");
        if(tile != null){ 
            this.ShootFireOnTile(tile);

            /*
        	Tile above = aboveTile(tile);

        	if(above != null){
        		Tile aboveAbove = aboveTile(above);

        		if(aboveAbove != null){
        			Debug.Log(aboveAbove.getTileID());
        			this.ShootWaterOnTile(aboveAbove.getTileID());
        		}
        		else{
        			Debug.Log(above.getTileID());
        			this.ShootWaterOnTile(above.getTileID());
        		}
        	}
        	else{
        		Debug.Log(tile.getTileID());
        		this.ShootWaterOnTile(tile.getTileID());
        	}
        	*/
        }
        else{
        	Debug.Log("TILE IS NULL IN INTERACTINFRONT");

        	
        }



    }

    public Tile aboveTile(Tile curTile){
    	RaycastHit hit;

		Debug.DrawRay(curTile.transform.position, curTile.transform.up, Color.red, 1, false);
		if(Physics.Raycast(curTile.transform.position, curTile.transform.up, out hit)){
			
			if(hit.collider.tag == "Tile"){
				Tile cur = hit.collider.gameObject.GetComponent<Tile>();

				if(cur != null && cur.enabled){
					Debug.Log(cur.getTileID());
					return cur;
				}
			}
		}

		return null;
    }
}
