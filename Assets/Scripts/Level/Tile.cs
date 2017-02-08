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
 

	// Use this for initialization
	void Start(){
		level = transform.parent.GetComponent<Level>();

		Vector3 tilePos = transform.position;
		tileID = (int)(tilePos.x  * GameManager.GRID_SIZE) + (int)tilePos.z;

		nIDList = fetchNeighborTiles();
		setMaterial();
	}

	
	// Update is called once per frame
	void Update () {
	
	}

	public int getTileID(){return tileID;}
	public List<int> getNTileIDList(){return this.nIDList;}



	public List<int> fetchNeighborTiles(){
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


		
		/*
		// Only add valid TileID's
		foreach(int n in result){
			//if(LevelManager.instance.getTileAt(n) != null)
			this.nIDList.Add(n);
		}
		*/

		return result;
	}

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