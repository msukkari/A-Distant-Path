using UnityEngine;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour {
	
	//private List<Tile> TileList;

	public Material GrassMat;

	private LevelManager lm = LevelManager.instance;


	// Use this for initialization
	void Start () {

		// Initialize me!
		lm.AttachLevel(this);
		lm.LoadTileList();

		//setTileMaterial();
		

	}
	
	// Update is called once per frame
	void Update () {

	}

	/*
	private void setTileMaterial(){
		List<Tile> tileList = lm.getTileList();

		for(int i = 0; i < tileList.Count; i++){
			tileList[i].GetComponent<Renderer>().material = GrassMat;
		}
	}
	*/

	//public List<Tile> getTileList(){return TileList;}


}
