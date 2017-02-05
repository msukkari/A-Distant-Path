using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour{


	void Start(){

	}


	public int getCurTileID(){
		return ((int)Math.Round((transform.position.x  * 10), MidpointRounding.AwayFromZero) + (int)Math.Round(transform.position.z, MidpointRounding.AwayFromZero));
	}

}
