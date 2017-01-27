using UnityEngine;
using System.Collections.Generic;

public class Level : MonoBehaviour {

	public int timeState; // 0 for past 1 for present
	public Material PastMat;
	public Material PresentMat;

	private List<Tile> TileList;

	// Use this for initialization
	void Start () {
		TileList = new List<Tile>();

		foreach(Transform child in transform){
			if(timeState == 0)
				child.GetComponent<Renderer>().material = PastMat;
			else 
				child.GetComponent<Renderer>().material = PresentMat;
				
			TileList.Add(child.GetComponent<Tile>());
		}

		print(TileList.Count);


	}
	
	// Update is called once per frame
	void Update () {

	}

	public Tile getTile(int i){
		foreach(Tile tile in TileList){
			if(tile.getTileID() == i)
				return tile;
		}

		return null;
	}

	public List<Tile> getTileList(){return TileList;}


}
