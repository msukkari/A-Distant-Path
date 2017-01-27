using UnityEngine;
using System;
using System.Collections;

public class Tile : MonoBehaviour {

	public int tileID;

	private Level level;
	private Boolean hasPlant;
	private Boolean hasSeed;


	// Use this for initialization
	void Start(){
		hasPlant = false;
		level = transform.parent.GetComponent<Level>();

		int diff;
		if(level.timeState == 0)
			diff = 0;
		else 
			diff = 40;


		Vector3 tilePos = transform.position;
		tileID = (int)((tilePos.x - diff) * 15) + (int)tilePos.z;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getTileID(){return tileID;}

	public void changeColor(string color){

		if(color == "blue")
    		transform.GetComponent<Renderer>().material.color = Color.blue;
    	else if(color == "red")
    		transform.GetComponent<Renderer>().material.color = Color.red;
    }

    public void setPlantState(Boolean state){hasPlant = state;}
    public Boolean getPlantState(){return this.hasPlant;}
    public void setSeedState(Boolean state){hasSeed = state;}
    public Boolean getSeedState(){return this.hasSeed;}
}
