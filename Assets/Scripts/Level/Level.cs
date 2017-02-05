using UnityEngine;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour {
	
	//private List<Tile> TileList;

	private LevelManager lm = LevelManager.instance;


	// Use this for initialization
	void Start () {

		// Initialize me!
		lm.AttachLevel(this);
		lm.LoadTileList();

	}
	
	// Update is called once per frame
	void Update () {

	}

	//public List<Tile> getTileList(){return TileList;}


}
