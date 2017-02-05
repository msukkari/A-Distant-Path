using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTransferManager : Photon.MonoBehaviour {

	public int cur;

	// Use this for initialization
	void Awake () {
		cur = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(!photonView.isMine){
			Debug.Log("Tile ID: " + cur);
		}
		*/


		if(Input.GetKeyDown("space"))
			GetComponent<PhotonView>().RPC("displayCur", PhotonTargets.All);
			

	}


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            // We own this player: send the others our data
            stream.SendNext(100);
        }
        else
        {
            // Network player, receive data
            cur = (int)stream.ReceiveNext();
        }
    }


    [PunRPC]
    void displayCur(){
    	Debug.Log(cur);
    }
}