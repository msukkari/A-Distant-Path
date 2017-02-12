using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


	// Main canvas object
	public Canvas MainCanvas;

	// GameManager GetInstanceGameManager
	private GameManager gm = GameManager.instance;


	public void LoadOnline() {

		// Initialize network connection
		gm.InitNetwork();

		// Load loading scene
		SceneManager.LoadScene((int) Scenes.Loading);	
 	}

 	public void LoadOffline(){
 		// load off line scene
 		Debug.Log("LOADING OFFLINE SCENE");
 		
 		gm.InitLevel(TimeStates.Offline);


 	}
}
