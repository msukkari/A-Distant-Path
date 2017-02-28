using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

	// GameManager GetInstanceGameManager
	private GameManager gm = GameManager.instance;

	// LevelManager instance
	private LevelManager lm = LevelManager.instance;

	// static instance of AIManager
	public static AIManager instance = null;	

	// Current
	public List<Enemy> enemies = new List<Enemy>();


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
		Level level = lm.getAttachedLevel();
		Debug.Log("AIManager.cs: Starting AI init for - " + level.name);
	}

	// recalculatePaths: re-calculate all AI's paths
	public void recalculatePaths(Tile tile){

		Debug.Log("recalculating..");

		// Loop all enemy
		foreach (Enemy enemy in enemies) {	

			Star star = enemy.behavior.GetComponent( typeof(Star) ) as Star;

			if (star != null) {
				star.test(tile);
			}

		}


	}	

	
	// Update is called once per frame
	void Update () {
		
	}
}
