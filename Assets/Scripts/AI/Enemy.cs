using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	// Attached behavior
	//public GameObject behavior;

	// AIManager instance
	private AIManager am = AIManager.instance;

	private LevelManager lm = LevelManager.instance;

	// public enum for current state
	public AIStates currentState;

	// current state class
	public AIState stateClass;	

	public float moveSpeed = 3.0f;

	// spawn tile of enemy
	private Tile spawnTile;

	// Use this for initialization
	void Start () {

		// add this to a list of enemies
		am.enemies.Add(this);

		// set the spawn tile
		spawnTile = getCurTile();

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

			case AIStates.WaterSearch:
				Debug.Log("--- AI STATE CHANGE: WaterSearch ---");
				stateClass = new AIState(new WaterSearch(this));
				break;

			case AIStates.ReturnSpawn:
				Debug.Log("--- AI STATE CHANGE: ReturnSpawn ---");
				stateClass = new AIState(new ReturnSpawn(this));
				break;

			case AIStates.RandomMovement:
				Debug.Log("--- AI STATE CHANGE: RandomMovement ---");
				stateClass = new AIState(new RandomMovement(this));
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

	public bool NeedToRecalculatePath(List<Node> path, int currentNode) {
		return path.Count != 0 && currentNode + 1 <= path.Count - 1 && 
			!path [currentNode + 1].tile.navigatable && lm.player.getCurTile() != path [currentNode + 1].tile;
	}

	public int getCurTileID(){
		return this.getCurTile() == null ? -1 : this.getCurTile().id; 
	}

	public Tile getSpawnTile() {
		return spawnTile;
	}

	
}
