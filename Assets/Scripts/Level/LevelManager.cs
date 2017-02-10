using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class LevelManager : MonoBehaviour {

	// static instance of LevelManager
	public static LevelManager instance = null;
	public GameObject playerPrefab;
	public GameObject elementManagerPrefab;

	private TimeStates TimeState;

	// TileList for given level
	private List<Tile> TileList;

	// Associated level attached to this manager
	private Level attachedLevel = null;

	TimeStates getTimeState(){
		return this.TimeState;
	}

	public void setTimeState(TimeStates state){this.TimeState = state;}


	void Awake() {
		// if the static class instance is null (singleton pattern)
		if (instance == null)
			instance = this;

		// if instance already exists and it's not this:
		else if (instance != this)

			// then destroy this. Enforces singletonPattern
			Destroy(gameObject);

		// Sets this to not be destroyed on scene reload
		DontDestroyOnLoad(gameObject);

		Debug.Log("level initialization...");

	}	

	public void LoadLevelScene(){
		GameObject ETManagerGO = (GameObject)PhotonNetwork.Instantiate("EventTransferManager", Vector3.zero, Quaternion.identity, 0);
		EventTransferManager ETManager = ETManagerGO.GetComponent<EventTransferManager>();

		GameObject elementManagerGO = Instantiate (elementManagerPrefab) as GameObject;
		elementManagerGO.transform.parent = this.gameObject.transform;

		GameObject player = Instantiate(playerPrefab, new Vector3(10f, 2.0f, 10f), Quaternion.identity) as GameObject;
		ETManager.player = player.GetComponent<Player>();

		DontDestroyOnLoad(ETManager);
		DontDestroyOnLoad(player);

		if(TimeState == TimeStates.Past){
			SceneManager.LoadScene((int)Scenes.Past);
		}
		else{
			SceneManager.LoadScene((int)Scenes.Present);
		}

	}

	// Attach a new Level object
	public void AttachLevel(Level level) {

		// Assign level to this manager
		this.attachedLevel = level;
	}

	// Get the Tile with the passed in ID, returns null if not found
	public Tile getTileAt(int id){
		foreach(Tile tile in TileList){
			if(tile.getTileID() == id)
				return tile;
		}

		return null;
	}

	// Load the tile list from level into TileList and initialize tiles
	public void LoadTileList() {
			
		// Instantiate new TileList
		TileList = new List<Tile>();

		// Loop and add all tiles
		foreach(Transform child in attachedLevel.transform){			
			TileList.Add(child.GetComponent<Tile>());
		}

	
		// Calculate the ID's of all the tiles (this must be done first in order for the neighbor method to work)
		foreach(Tile tile in TileList){
			tile.calcID();
		}
		foreach(Tile tile in TileList){
			tile.initTile();
		}

		foreach(Tile tile in TileList) {
			// Attaches random elements to tiles other than the player's current position. Was used for testing 
			// out the level.
			/*if(tile.id != playerPrefab.GetComponent<Player>().getCurTileID()) {

				ElementType elementType = (ElementType)(Random.Range (0, Enum.GetNames (typeof(ElementType)).Length + 1) - 1);

				if (elementType >= 0) {
					CreateElementAtTile (tile, elementType);
				}
			}*/

			Collider[] neighbors = Physics.OverlapSphere(tile.transform.position, 1.0f);
			for (int i = 0; i < neighbors.Length; i++) {
				Tile other = neighbors[i].gameObject.GetComponent<Tile> ();

				if (other != null && !tile.neighbors.Contains(other) && other != tile) {
					tile.neighbors.Add (other);
				}
			}
		}
	}

	// Get TileList
	public List<Tile> getTileList(){return TileList;}

	public static void CreateElementAtTile(Tile tile, ElementType elementType) {
		GameObject elementCreated = Instantiate (ElementManager.elementSpawnDictionary[elementType], tile.transform);
		elementCreated.transform.position = new Vector3(tile.transform.position.x, elementCreated.transform.position.y, tile.transform.position.z);
		tile.element = elementCreated.GetComponent<Element> ();

		tile.SetNavigatable (false);
	}
	
	// Update is called once per frame
	void Update () { }
}

public enum TimeStates {
	Past,
	Present
};