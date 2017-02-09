using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Player : MonoBehaviour{


	private float elementTimeCount = -1f;

	private const int PICKUP_TIME_THRESHOLD = 1;

	void Start(){

	}



	void Update(){
		if(Input.GetKeyDown(KeyCode.E))
			printNTiles();
		
		CheckElementPickup();
	}


	private void CheckElementPickup() {

		// check if player is attempting to collect water
		if (!Input.GetKey(KeyCode.F)) {
			elementTimeCount = 0;
			return;
		}

		elementTimeCount += Time.deltaTime;

		if (elementTimeCount >= PICKUP_TIME_THRESHOLD) {
			Debug.Log("element pickup");
			printNTiles();
			elementTimeCount = 0;

			if(next2Resource()){
				Debug.Log("PLAYER IS NEXT TO A RESOURCE");
			}
		}
	}

	private bool next2Resource(){
		Tile curTile = getCurTile();
		List<int> nList = curTile.getNTileIDList();

		foreach(int id in nList){
			Tile neighbor = LevelManager.instance.getTileAt(id);
			if(neighbor.state == Tile.State.Water || neighbor.state == Tile.State.Fire){
				return true;
			}

		}

		return false;
	}
	
	/* 
	// OLD METHOD
	public int getCurTileID(){
		int x = (int)Math.Round((transform.position.x), MidpointRounding.AwayFromZero) * GameManager.GRID_SIZE;
		int z = (int)Math.Round(transform.position.z, MidpointRounding.AwayFromZero);
		Debug.Log("X CORD: " + x);
		Debug.Log("Z CORD: " + z);

		return x + z;
	}
	*/

	public int getCurTileID(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if(cur != null)
				return cur.tileID;
		}

		return -1;
	}

	public Tile getCurTile(){
		return LevelManager.instance.getTileAt(this.getCurTileID());
	}

	// Using this to DEBUG
	public void printNTiles(){

		int curTileID = getCurTileID();
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
