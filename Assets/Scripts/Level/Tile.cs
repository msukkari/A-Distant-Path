using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public int tileID;
	public enum State {
		Grass,
		Water,
		Fire
	};
	public State state;

	public Material GrassMat;
	public Material WaterMat;
	public Material FireMat;

	private Level level;
	private List<int> nIDList;
 

	void Start(){

	}

	
	// Update is called once per frame
	void Update (){
	
	}

	// PUBLIC METHODS //
	public int getTileID(){return tileID;} // getter for tileID
	public List<int> getNTileIDList(){return this.nIDList;} // getter for neighbor tile ID list

	// calculates and sets the tile ID
	public void calcID(){
		this.tileID = (int)(transform.position.x  * GameManager.GRID_SIZE) + (int)transform.position.z;
	}


	// initialization of the tile goes here (tile ID calculation must be done before this method since some methods in init require all tile ID's to be calculated)
	public void initTile(){
		// Get the neighbor list and then validate it
		this.fetchNeighborTiles();
		this.validateNList();

		// Set the material of the tile based on what state it is
		this.setMaterial();
	}


	// PRIVATE METHODS //

	// Removes invalid tile ID's (ID's that don't represent an actual tile)
	private void validateNList(){
		List<int> validList = new List<int>();

		for(int i = 0; i < nIDList.Count; i++){
			Tile tile = LevelManager.instance.getTileAt(nIDList[i]);
			if(tile != null){
				validList.Add(tile.getTileID());
			}
		}

		this.nIDList = validList;
	}


	// Gets all the neighboring tile ID's and puts them in nIDList (NOTE: list must be validated! See valideNList())
	private void fetchNeighborTiles(){
		List<int> result = new List<int>();
		int maxTileID = (GameManager.GRID_SIZE * GameManager.GRID_SIZE) - 1;

		// Forward is considered to be in the positive z-axis
		// Right is considered to be in the positive x-axis

		int forward = this.tileID + 1;
		if(forward >= 0 && forward <= maxTileID) result.Add(forward);

		int back = this.tileID - 1;
		if(back >= 0 && back <= maxTileID) result.Add(back);

		int right = this.tileID + GameManager.GRID_SIZE;
		if(right >= 0 && right <= maxTileID) result.Add(right);

		int left = this.tileID - GameManager.GRID_SIZE;
		if(left >= 0 && left <= maxTileID) result.Add(left);

		int forward_right = this.tileID + 1 + GameManager.GRID_SIZE;
		if(forward_right >= 0 && forward_right <= maxTileID) result.Add(forward_right);

		int forward_left = this.tileID + 1 - GameManager.GRID_SIZE;
		if(forward_left >= 0 && forward_left <= maxTileID) result.Add(forward_left);

		int back_right = this.tileID - 1 + GameManager.GRID_SIZE;
		if(back_right >= 0 && back_right <= maxTileID) result.Add(back_right);

		int back_left = this.tileID - 1 - GameManager.GRID_SIZE;
		if(back_left >= 0 && back_left <= maxTileID) result.Add(back_left);

		this.nIDList = result;
	}

	// Sets the material of the tile based on the state it's in 
	private void setMaterial(){
		Renderer renderer = GetComponent<Renderer>();
		switch(this.state){
			case State.Grass:
				renderer.material = GrassMat;
				break;
			case State.Water:
				renderer.material = WaterMat;
				break;
			case State.Fire:
				renderer.material = FireMat;
				break;
		}
	}





}