using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour{


	void Start(){

	}


	public int getCurTileID(){
		int x = (int)Math.Round((transform.position.x), MidpointRounding.AwayFromZero) * 10;
		int z = (int)Math.Round(transform.position.z, MidpointRounding.AwayFromZero);
		Debug.Log("X CORD: " + x);
		Debug.Log("Z CORD: " + z);

		return x + z;
	}

}
