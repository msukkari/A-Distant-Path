using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
* NetworkManager: Handles network instantiation and vigilance.
* Initialized on call from GameManger class.
* NOTE: there is only ONE static instance of NetworkManager (I.E the class is a singleton)
*
* NOTE ON USAGE: Like the GameManager, ensure the NetworkManager script is located in
* a bare-bones prefab. No loader is required for NetworkManager, GameManger handles
* this process. 
*
* SEE: GameManager.cs
**/

public class NetworkManager : MonoBehaviour {

	// static instance of NetworkManager
	public static NetworkManager instance = null;

	// boolean to run game in offlinemode
	private readonly bool OFFLINEMODE = false;

	// GameManager instance
	private GameManager gm = GameManager.instance;

	// The other player in the game
	private PhotonPlayer otherPlayer;

	// Holds the current connection state
	private bool isConnected = false;


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

		Debug.Log("network initializing...");

		// Connect to photon
		Connect();
	}

	// Connect: connect to the photon cloud
	void Connect() {

		// for testing purposes... -- ON PRODUCTION BUILD, REMOVE
		PhotonNetwork.offlineMode = OFFLINEMODE;

		// Connect to photon network...
		PhotonNetwork.ConnectUsingSettings(gm.GetBuild());
	}

	// <BUILT IN CALL> OnGUI: Displays photon connect details
	void OnGUI() {
		GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
	}

	// <BUILT IN CALL> OnJoinedLobby: Attempts to connect client to a random lobby
	void OnJoinedLobby() {	

		Debug.Log("attempting to join room....");

		// Attempt to join random room
		PhotonNetwork.JoinRandomRoom();
	}


	// <BUILT IN CALL> OnPhotonRandomJoinFailed: If no room exists, a room must be created
	void OnPhotonRandomJoinFailed() {
		Debug.Log("Join Failed");
		Debug.Log("Creating new room...");
		PhotonNetwork.CreateRoom(null);
	}

	// <BUILT IN CALL> OnJoinedRoom: If a player successfully joins a room
	void OnJoinedRoom() {
		Debug.Log("Room Joined.");

		// Attempt to start game
		OnStart();
	}	

	// <BUILT IN CALL> OnPhotonPlayerConnected: 
	void OnPhotonPlayerConnected(PhotonPlayer newPlayer) {

		// Attempt to start game
		OnStart();
	}

	// OnStart: occurs when both players have joined the room. The game is ready to go :)
	void OnStart() {

		// Get all other players in the room
		PhotonPlayer[] players = PhotonNetwork.otherPlayers;

		// Ensure there are only two players in the room
		if (players.Length != 1) {
			Debug.Log("Player count not fulfilled..");
			return;
		}

		// instantiate the other player
		otherPlayer = players[0];

		Debug.Log("~~READY~~");

		// set connection state
		isConnected = true;

		// Load the main scene
		SceneManager.LoadScene((int) Scenes.MainScene);
	}

	// getConnectionStatus: returns if connection is up
	public bool getConnectionStatus() {
		return isConnected;
	}

}


