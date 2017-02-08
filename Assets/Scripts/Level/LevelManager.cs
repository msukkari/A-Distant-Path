using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum TimeStates {
		Past,
		Present
};

public class LevelManager : MonoBehaviour {

	// static instance of LevelManager
	public static LevelManager instance = null;
	public GameObject playerPrefab;


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

	public Tile getTileAt(int id){
		foreach(Tile tile in TileList){
			if(tile.getTileID() == id)
				return tile;
		}

		return null;
	}

	// Load the tile list from level into TileList
	public void LoadTileList() {
			
		// Instantiate new TileList
		TileList = new List<Tile>();

		// Loop and add all tiles
		foreach(Transform child in attachedLevel.transform){			
			TileList.Add(child.GetComponent<Tile>());
		}

		/*
		// Calculate the neighboring tiles for each tile
		foreach(Tile tile in TileList){
			tile.fetchNeighborTiles();
		}
		*/
	}

	// Get TileList
	public List<Tile> getTileList(){return TileList;}

	
	// Update is called once per frame
	void Update () { }
}
