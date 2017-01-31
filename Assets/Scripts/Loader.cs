using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
* Loader: The loader is a script that simply loads the GameManger.
* Attach this script to absolute game objects ONLY. Each scene should
* have one Loader present.
**/

public class Loader : MonoBehaviour {

	// GameManager prefab to instantiate (set in GUI)
	public GameObject gameManager;  

	void Awake() {

		// Check if a GameManager has already been instantiated
		if (GameManager.instance == null) 
			
			// Instantiate gameManager prefab
			Instantiate(gameManager);

	}

}
