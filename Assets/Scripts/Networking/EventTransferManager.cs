using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTransferManager : Photon.MonoBehaviour {

	public int cur;

	public Player player;

	// get LevelManager
	private LevelManager lm = LevelManager.instance;


	// Use this for initialization
	void Awake () {

	}
	
	// Update is called once per frame
	void Update () {

		if(photonView.isMine && Input.GetKeyDown("space")) {
		
			// Transfer
			GetComponent<PhotonView>().RPC("Event",PhotonTargets.Others, new object[]{player.getCurTileID()});

		}
		
	}


	[PunRPC]
	public void Event(int TileID){

		Tile tile = lm.getTileAt(TileID);
		tile.GetComponent<Renderer>().material.color = Color.blue;

		foreach(int n in tile.getNTileIDList()){
			Tile neighbor = lm.getTileAt(n);

			neighbor.GetComponent<Renderer>().material.color = Color.blue;
		}


		Debug.Log(TileID);
	}	

}