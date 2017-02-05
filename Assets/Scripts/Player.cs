using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{


	void Start(){

	}


	public int getCurTileID(){
		return ((int)(transform.position.x  * 10) + (int)transform.position.z);
	}

}
