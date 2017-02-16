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

		if(photonView.isMine && Input.GetKeyDown(KeyCode.C) && player.getCurElementType() == ElementType.Transfer){
			Debug.Log("CALLING TRANSFER TILE CHECK");
			GetComponent<PhotonView>().RPC("transferTileCheck",PhotonTargets.Others);
		}
		
	}


	[PunRPC]
	public void Event(int TileID){

		Tile tile = lm.getTileAt(TileID);

		if(tile != null){
			tile.GetComponent<Renderer>().material.color = Color.blue;
		

			// Loop through neighboring tile's too
			foreach(Tile neighbor in tile.neighbors){
				if(neighbor != null){
					neighbor.GetComponent<Renderer>().material.color = Color.blue;
				}
			}
		}	


		Debug.Log(TileID);
	}	


	// Check if this player is on a transfer tile, and if so c
	[PunRPC]
	public void transferTileCheck(){
		if(player == null){
			Debug.Log("PLAYER NOT FOUND!");
			return;
		}
		if(player.getCurElementType() == ElementType.Transfer){
			Debug.Log("TRANSFER ATTEMPT DETECTED - SWAPPING");
			GetComponent<PhotonView>().RPC("swapElements",PhotonTargets.Others);
		}
		else{
			Debug.Log("NOT STANDING ON A TRANSFER ELEMENT - TRANSFER NOT DONE");
		}
	}

	[PunRPC]
	public void swapElements(){
		Debug.Log("SWAPPING");
	}

}