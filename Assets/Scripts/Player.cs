using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour{


	void Start(){

	}

	void Update(){
		if(Input.GetKeyDown(KeyCode.E))
			printNTiles();
	}

	public int getCurTileID(){
		int x = (int)Math.Round((transform.position.x), MidpointRounding.AwayFromZero) * GameManager.GRID_SIZE;
		int z = (int)Math.Round(transform.position.z, MidpointRounding.AwayFromZero);
		Debug.Log("X CORD: " + x);
		Debug.Log("Z CORD: " + z);

		return x + z;
	}

	// Using this to DEBUG
	public void printNTiles(){
		int curTileID = getCurTileID();
		Tile curTile = LevelManager.instance.getTileAt(curTileID);
		List<int> neighborList = curTile.getNTileIDList();


		foreach(int n in neighborList)
			Debug.Log(n);

	}

}
