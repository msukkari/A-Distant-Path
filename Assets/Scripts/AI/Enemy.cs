using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	// Attached behavior
	//public GameObject behavior;

	// AIManager instance
	private AIManager am = AIManager.instance;

	// public enum for current state
	public AIStates currentState;

	// current state class
	public AIState stateClass;	


	// Use this for initialization
	void Start () {

		// add this to a list of enemies
		am.enemies.Add(this);

		// initialize this enemies current state
		initState();

		//GameObject behaviorGO = Instantiate (behavior) as GameObject;
		//behaviorGO.transform.parent = this.gameObject.transform;

	}
	
	// Update is called once per frame
	void Update () {
		stateClass.Update();
	}

	// setState: set the current AIState of this enemy
	public void setState(AIStates state) {
		this.currentState = state;
		initState();
	}

	// initState: initializes a new ai-state
	private void initState() {

		// switch-case for current state
		switch (currentState) {

			case AIStates.FollowPlayer:
				Debug.Log("--- AI STATE CHANGE: FollowPlayer ---");
				stateClass = new AIState(new PlayerFollow(this));
				break;

			default:
				Debug.Log("--- DEFAULT AI STATE CHANGE: FollowPlayer ---");
				stateClass = new AIState(new PlayerFollow(this));
				break;
		}
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
