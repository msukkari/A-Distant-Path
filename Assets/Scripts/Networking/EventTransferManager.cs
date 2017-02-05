using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTransferManager : Photon.MonoBehaviour {

	public int cur;


	private Player player;

	// Use this for initialization
	void Awake () {
		if(photonView.isMine)
			player = GameObject.Find("Name").GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {

		if(photonView.isMine && Input.GetKeyDown("space"))
			GetComponent<PhotonView>().RPC("Event",PhotonTargets.Others, new object[]{player.getCurTileID()});
		
	}


	[PunRPC]
	public void Event(int TileID){
		Debug.Log(TileID);
	}	

}