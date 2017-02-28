using UnityEngine;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour {
	
	//private List<Tile> TileList;

	private LevelManager lm = LevelManager.instance;
	private AIManager am = AIManager.instance;

	public string name = "";


	// Use this for initialization
	void Start() {

		Debug.Log("Level.cs: Level created");

		// Initialize me!
		lm.AttachLevel(this);
		lm.LoadTileList();

		
		// ~~ Do all level initialization shit here ~~ //


		am.init();



	}
	
	// Update is called once per frame
	void Update () {

	}

}
