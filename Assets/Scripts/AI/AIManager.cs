using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	// GameManager GetInstanceGameManager
	private GameManager gm = GameManager.instance;

	private LevelManager lm = LevelManager.instance;

	// static instance of AIManager
	public static AIManager instance = null;


	// Awake is called before Start function
	void Awake() {

		// if the static class instance is null (singleton pattern)
		if (instance == null)
			instance = this;

		// if instance already exists and it's not this:
		else if (instance != this)
			Destroy(gameObject);

		// Sets this to not be destroyed on scene reload
		DontDestroyOnLoad(gameObject);

		Debug.Log("AIManager.cs: AI manager initialized");
	}	

	// init: Initialze all level-specific AI related shit here
	public void init() {	
		

	}


	public void testaa() {

		Debug.Log("here");
		
		List<Tile> list = lm.getTileList();


		foreach (Tile tile in list) {
			Debug.Log("here");
		}

	}

	
	// Update is called once per frame
	void Update () {
		
	}
}
