using UnityEngine;
using System.Collections.Generic;
using System;

public class Level : MonoBehaviour {
	
	private List<Tile> TileList;

	// Use this for initialization
	void Start () {
		TileList = new List<Tile>();

		foreach(Transform child in transform){			
			TileList.Add(child.GetComponent<Tile>());
		}


	}
	
	// Update is called once per frame
	void Update () {

	}

	public List<Tile> getTileList(){return TileList;}


}
