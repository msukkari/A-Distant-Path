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

	public int getIDNew(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if(cur != null)
				return cur.tileID;
		}

		return -1;
	}

	// Using this to DEBUG
	public void printNTiles(){

		int curTileID = getIDNew();
		Tile curTile = LevelManager.instance.getTileAt(curTileID);
		if(curTile == null){
			Debug.Log("TILE NOT FOUND");
			return;
		}
		List<int> neighborList = curTile.getNTileIDList();


		foreach(int n in neighborList)
			Debug.Log(n);

	}

}
