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

		if(photonView.isMine && Input.GetKeyDown(KeyCode.C) && player.getCurTile().element.elementType == ElementType.Transfer){
			Debug.Log("CALLING TRANSFER TILE CHECK");

			int water = player.elementsInventory.ContainsKey(ElementType.Water) ? player.elementsInventory[ElementType.Water] : 0;
			int fire = player.elementsInventory.ContainsKey(ElementType.Fire) ? player.elementsInventory[ElementType.Fire] : 0;
			GetComponent<PhotonView>().RPC("transferTileCheck",PhotonTargets.Others, new object[]{fire, water});
		}


		if(photonView.isMine){
			//Debug.Log(player.getCurTileID());
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


	// Checks to see if the current tile has a Transfer element, if it does then call the other player's transfer RPC
	// NOTE: This method has a lot of Debug.Log's which I used for debugging - not 100% necessary 
	[PunRPC]
	public void transferTileCheck(int fire, int water){
		Debug.Log("RECEIEVED RPC CALL FOR TRANSFER!");
		GameObject play = GameObject.FindGameObjectsWithTag("Player")[0];
		Player curPlayer = play.GetComponent<Player>();

		if(curPlayer != null){
			Debug.Log("PLAYER COMPONENT FOUND");
			Tile curTile = curPlayer.getCurTile();
			if(curTile != null){
				Debug.Log("TILE FOUND");

				Element curElement = curTile.element;
				if(curElement != null){
					Debug.Log("ELEMENT FOUND!");
					if(curElement.elementType == ElementType.Transfer){
						Debug.Log("ELEMENT IS A TRANSFER ELEMENT!");
						int curWater = curPlayer.elementsInventory.ContainsKey(ElementType.Water) ? curPlayer.elementsInventory[ElementType.Water] : 0;
						int curFire = curPlayer.elementsInventory.ContainsKey(ElementType.Fire) ? curPlayer.elementsInventory[ElementType.Fire] : 0;
						curPlayer.elementsInventory[ElementType.Water] = water;
						curPlayer.elementsInventory[ElementType.Fire] = fire;

						GetComponent<PhotonView>().RPC("transferResources",PhotonTargets.Others, new object[]{curFire, curWater});
					}
				}
				else{
					Debug.Log("ELEMENT NOT FOUND");
				}
			}
		}
		else{
			Debug.Log("PLAYER COMPONENT NOT FOUND");
		}

	}


	[PunRPC]
	public void transferResources(int fire, int water){
		Debug.Log("RECEIEVED TRANSFER COMFIRMATION");
		//Debug.Log("OTHER PLAYER HAS " + fire + " FIRE AND " + water + " WATER");

		player.elementsInventory[ElementType.Water] = water;
		player.elementsInventory[ElementType.Fire] = fire;
	}

}