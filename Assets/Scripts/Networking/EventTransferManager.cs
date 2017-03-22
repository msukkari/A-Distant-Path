using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTransferManager : Photon.MonoBehaviour {

	public int cur;

	public GameObject playerGO;
	public Player player;


	// get LevelManager
	private LevelManager lm = LevelManager.instance;
	private Vector3 recentTransferPos;
	private bool transferHighlighted;


	private bool otherPlayerPressing;


	// Use this for initialization
	void Awake () {
		this.transferHighlighted = false;
		this.otherPlayerPressing = false;
	}
	
	// Update is called once per frame
	void Update () {

		if(playerGO == null){
			playerGO = GameObject.FindGameObjectWithTag("Player");
			player = playerGO.GetComponent<Player>();
		}

		if(photonView.isMine){

			Debug.Log(this.player.otherPlayerPressingTransfer);


			/* Added for debugging, not necessary anymore
			if(Input.GetKeyDown("space")) {
			
				// Transfer
				GetComponent<PhotonView>().RPC("Event",PhotonTargets.Others, new object[]{player.getCurTileID()});

			}
			*/
			//Debug.Log(player.getCurTile().element);
			if(player.getCurTile().element != null && player.getCurTile().element.elementType == ElementType.Transfer){

				Debug.Log("PLAYER IS ON A TRANSFER TILE");

				recentTransferPos = player.getCurTile().transform.position;



				if(Input.GetButton("BButton") && !this.player.hasTransfered){
					if(this.player.otherPlayerPressingTransfer){
						// Transfer resources
						Debug.Log("CALLING SEND ELEMS");
						GetComponent<PhotonView>().RPC("sendElems", PhotonTargets.Others);
						this.player.hasTransfered = true;
					}
					else{
						// Change other players pressed
						Debug.Log("Calling otherPlayerPressTransfer RPC");
						GetComponent<PhotonView>().RPC("otherPlayerPressTransfer", PhotonTargets.Others);
					}
				}
				else if(!Input.GetButtonUp("BButton")){
					this.player.hasTransfered = false;
				}





				/*
				if(transferHighlighted == false){
					Debug.Log("CALLING pOnTransfer");
					GetComponent<PhotonView>().RPC("pOnTransfer",PhotonTargets.Others, new object[]{recentTransferPos});
					transferHighlighted = true;
				}


				if(Input.GetKeyDown(KeyCode.C) || Input.GetButtonDown("AButton")){
					Debug.Log("CALLING TRANSFER TILE CHECK");

					int water = player.elementsInventory.ContainsKey(ElementType.Water) ? player.elementsInventory[ElementType.Water] : 0;
					int fire = player.elementsInventory.ContainsKey(ElementType.Fire) ? player.elementsInventory[ElementType.Fire] : 0;
					GetComponent<PhotonView>().RPC("transferTileCheck",PhotonTargets.Others, new object[]{fire, water});
				}
				*/
			}
			else{

				if(transferHighlighted == true){
					GetComponent<PhotonView>().RPC("pOffTransfer",PhotonTargets.Others, new object[]{recentTransferPos});
					transferHighlighted = false;
				}
			}

		}
	}


	[PunRPC]
	public void otherPlayerPressTransfer(){
		Debug.Log("otherPlayerPressTransfer being called");

		if(player == null){
			Debug.Log("PLAYER IS NULL IN otherPlayerPressTransfer");
		}
		this.player.otherPlayerPressingTransfer = true;
	}


	public void OnMetalRust(Vector3 pos){
		if(photonView.isMine){
			GetComponent<PhotonView>().RPC("rustMetalCube",PhotonTargets.Others, new object[]{pos});
		}
	}

	public void OnStumpWater(Vector3 pos){
			GetComponent<PhotonView>().RPC("growTree",PhotonTargets.Others, new object[]{pos});
	}

	public void OnSaplingFire(Vector3 pos){
			GetComponent<PhotonView>().RPC("destroyTree",PhotonTargets.Others, new object[]{pos});
	}

	public void OnSandFire(int tileID) {
		GetComponent<PhotonView>().RPC("glassify",PhotonTargets.Others, new object[]{tileID});
	}

	public void OnLoseElement(int tileID) {
		GetComponent<PhotonView>().RPC("loseElement",PhotonTargets.Others, new object[]{tileID});
	}


	[PunRPC]
	public void sendElems(){
		Debug.Log("INSIDE SENDELEMS");
		GameObject play = GameObject.FindGameObjectsWithTag("Player")[0];
		Player curPlayer;

		if(play != null){
			curPlayer = play.GetComponent<Player>();
		}
		else{
			Debug.Log("Player gameobject not bad");
			return;
		}

		if(curPlayer != null){
			curPlayer.otherPlayerPressingTransfer = false;
			curPlayer.hasTransfered = false;

			int curWater = curPlayer.elementsInventory.ContainsKey(ElementType.Water) ? curPlayer.elementsInventory[ElementType.Water] : 0;
			int curFire = curPlayer.elementsInventory.ContainsKey(ElementType.Fire) ? curPlayer.elementsInventory[ElementType.Fire] : 0;

			GetComponent<PhotonView>().RPC("recieveElemsFirst",PhotonTargets.Others, new object[]{curFire, curWater});
		}
		else{
			Debug.Log("PLAYER COMPONENT NOT FOUND");
		}

	}

	[PunRPC]
	public void recieveElemsFirst(int fire, int water){
		Debug.Log("INSIDE RECIEVEELEMSFIRST");

		GameObject play = GameObject.FindGameObjectsWithTag("Player")[0];
		Player curPlayer;

		if(play != null){
			curPlayer = play.GetComponent<Player>();
		}
		else{
			Debug.Log("Player gameobject not bad");
			return;
		}

		if(curPlayer != null){
			curPlayer.hasTransfered = true;

			int curWater = curPlayer.elementsInventory.ContainsKey(ElementType.Water) ? curPlayer.elementsInventory[ElementType.Water] : 0;
			int curFire = curPlayer.elementsInventory.ContainsKey(ElementType.Fire) ? curPlayer.elementsInventory[ElementType.Fire] : 0;

			GetComponent<PhotonView>().RPC("recieveElemsSecond",PhotonTargets.Others, new object[]{curFire, curWater});
			curPlayer.otherPlayerPressingTransfer = false;

			curPlayer.elementsInventory[ElementType.Water] = water;
			curPlayer.elementsInventory[ElementType.Fire] = fire;
		}
		else{
			Debug.Log("PLAYER COMPONENT NOT FOUND");
		}
	}

	[PunRPC]
	public void recieveElemsSecond(int fire, int water){
		Debug.Log("INSIDE RECIEVEELEMSSECOND");

		GameObject play = GameObject.FindGameObjectsWithTag("Player")[0];
		Player curPlayer;

		if(play != null){
			curPlayer = play.GetComponent<Player>();
		}
		else{
			Debug.Log("Player gameobject not bad");
			return;
		}

		if(curPlayer != null){
			curPlayer.elementsInventory[ElementType.Water] = water;
			curPlayer.elementsInventory[ElementType.Fire] = fire;

			curPlayer.otherPlayerPressingTransfer = false;
		}
		else{
			Debug.Log("PLAYER COMPONENT NOT FOUND");
		}
	}



	[PunRPC]
	public void rustMetalCube(Vector3 pos){
		Debug.Log("METAL CUBE HAS BEEN RUSTED!");
		Tile tile = lm.getTileAt(pos);



		// AT THIS POINT, THE TURTLE AI WILL NEED TO RECALCULATE
		
		AIManager.instance.AIStateEvent(AIEvents.OnMetalCubeRust);

		if(tile != null){

			if(tile.element != null){
				if(tile.element.elementType == ElementType.MetalCube){
					tile.LoseElement();
				}
				else{
					Debug.Log("NOT A METAL CUBE!");
				}
			}
			else{
				Debug.Log("TILE HAS NO ELEMENT IN RUST METAL CUBE");
			}
		}
		else{
			Debug.Log("TILE NOT FOUND IN RUST METAL CUBE");
		}

	}

	[PunRPC] void glassify(int tileID){
		Debug.Log("FIRE HAS BEEN ADDED TO SAND!");
		Tile tile = lm.getTileAt(tileID);

		if(tile != null){
			tile.GainElement(ElementType.Glass);
		}
		else{
			Debug.Log("TILE NOT FOUND IN GLASSIFY");
		}
	}

	[PunRPC] void loseElement(int tileID){
		Debug.Log("FIRE HAS BEEN ADDED TO SAND!");
		Tile tile = lm.getTileAt(tileID);

		if(tile != null){
			tile.LoseElement();
		}
		else{
			Debug.Log("TILE NOT FOUND IN GLASSIFY");
		}
	}

	[PunRPC] void growTree(Vector3 pos){
		Debug.Log("SAPLING HAS BEEN WATERED!");
		Tile tile = lm.getTileAt(pos);

		if(tile != null){
			tile.GainElement(ElementType.BigTree);
		}
		else{
			Debug.Log("TILE NOT FOUND IN GROWTREE");
		}
	}

	[PunRPC]
	void destroyTree(Vector3 pos){
		Debug.Log("BIG TREE BEING DESTROYED");
		Tile tile = lm.getTileAt(pos);

		if(tile != null && tile.element != null && tile.element.elementType == ElementType.BigTree){
			tile.LoseElement();
		}
		else{
			Debug.Log("ELEMENT ISNT A BIG TREE IS DESTORY TREE");
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


	// Called when the other player is on a resource transfer tile, passes in the tileID
	[PunRPC]
	public void pOnTransfer(Vector3 pos){
		Tile tile = lm.getTileAt(pos);

		if(tile != null){

			if(tile.element != null){
				if(tile.element.elementType == ElementType.Transfer){
					tile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.red;
				}
				else{
					Debug.Log("NOT A TRANSFER TILE!");
				}
			}
			else{
				Debug.Log("TILE HAS NO ELEMENT IN pOnTransfer");
			}
		}
		else{
			Debug.Log("TILE NOT FOUND IN pOnTransfer");
		}
	}


	// Called when the other player is not on a resource transfer tile, passes in the tileID of the transfer tile it left
	[PunRPC]
	public void pOffTransfer(Vector3 pos){
		Tile tile = lm.getTileAt(pos);

		if(tile != null){

			if(tile.element != null){
				if(tile.element.elementType == ElementType.Transfer){
					tile.gameObject.transform.GetChild(0).GetComponent<Renderer>().material.color = Color.white;
				}
				else{
					Debug.Log("NOT A TRANSFER TILE!");
				}
			}
			else{
				Debug.Log("TILE HAS NO ELEMENT IN pOnTransfer");
			}
		}
		else{
			Debug.Log("TILE NOT FOUND IN pOnTransfer");
		}
	}

}



