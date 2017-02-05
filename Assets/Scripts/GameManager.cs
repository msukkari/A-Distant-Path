using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
* GameManager: The game manager is is the core absolute in the game. It is instantiated
* on boot and persists regardless of what scene is loaded.
* NOTE: there is only ONE static instance of GameManger (I.E the class is a singleton)
*
* NOTE ON USAGE: Ensure this GameManger script is located in a bare-bones prefab. 
* To initialize, attach the Loader.cs script onto a absolute game object in a given
* scene (like the main camera). Then attach the GameManager prefab onto said script.
*
* NOTE ON SCENE INITIALIZATION: On initialization, GameManager will automatically load
* Scene 0 (Main Menu).
**/

// all game scenes
public enum Scenes {
		MainMenu,
		Loading,
		MainScene,
		Past,
		Present
};

public class GameManager : MonoBehaviour {

	// game build
	private readonly string BUILD = "1.0.1";

	// static instance of GameManager
	public static GameManager instance = null;


	// --- NETWORK ---

	// NetworkManager GameObject
	public GameObject networkObject;

	// NetworkManager instance
	private NetworkManager network;

	// ---------------


	// --- LEVEL ---

	// LevelManager GameObject
	public GameObject levelObject;

	// LevelManager instance
	private LevelManager level;
	// ---------------

	// Awake is called before Start function
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

		// --- initiate game here ---
		InitGame();
	}


	// InitGame: initializes the game scene
	void InitGame() {
		Debug.Log("initializing game...");
		Debug.Log(Scenes.MainMenu);
		SceneManager.LoadScene((int) Scenes.MainMenu);		
	}	

	// InitNetowrk: initializes the networking (currently called on "play" button)
	public void InitNetwork() { 
				
		// Check if NetworkManager has already be instansiated
		if (NetworkManager.instance == null)	

			// instantiate the network prefab
			Instantiate(networkObject);

		// get the class instance
		network = NetworkManager.instance;
	}	

	// InitLevel: initializes the level following network initialization
	public void InitLevel(TimeStates TimeState) {
		
		// check if LevelManager is already instantiated
		if (LevelManager.instance == null)	

			// instantiate the level manager
			Instantiate(levelObject);

		// get class instance

		PhotonNetwork.isMessageQueueRunning = false;
		level = LevelManager.instance;
		level.setTimeState(TimeState);
		level.LoadLevelScene();
		PhotonNetwork.isMessageQueueRunning = true;
		

		Debug.Log(TimeState);
	}


	// GetBuild: return the current game build
	public string GetBuild() {
		return BUILD;
	}
	
	// Update is called once per frame
	void Update () { }
}
