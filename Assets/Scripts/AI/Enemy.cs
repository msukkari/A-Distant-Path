using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	public AIStates defaultState;

	// Attached behavior
	//public GameObject behavior;

	// AIManager instance
	private AIManager am = AIManager.instance;

	AIState test;


	// Use this for initialization
	void Start () {

		// add this to a list of enemies
		am.enemies.Add(this);

		//GameObject behaviorGO = Instantiate (behavior) as GameObject;
		//behaviorGO.transform.parent = this.gameObject.transform;
		
		test = new AIState(new StarTest(this));

	}
	
	// Update is called once per frame
	void Update () {
		test.Update();
	}

	public Tile getCurTile(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if(cur != null)
				return cur;
		}

		return null;
	}

	public int getCurTileID(){
		return this.getCurTile() == null ? -1 : this.getCurTile().id; 
	}

	
}
