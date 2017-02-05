using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileIDFetch : MonoBehaviour {

	private Player player;
	public int curTileID;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		curTileID = player.getCurTileID();
	}
}
