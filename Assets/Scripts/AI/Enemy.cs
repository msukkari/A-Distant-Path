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
	public AIStates currentState = AIStates.Idle;
	public AIStates initialState = AIStates.Idle;

	// current state class
	public AIState stateClass;	

	// speed of enemy
	public float moveSpeed = 3.0f;
	public float rotateSpeed = 2.0f;

	// list of enemy waypoints
	public List<Tile> waypoints = new List<Tile>();

	// spawn tile of enemy
	private Tile spawnTile;
	private Tile lastValidTile;

	public bool isShrunk = false;
	public bool isGrown = false;
	public bool foundWater = false;
	public bool frozePlayer = false;
	public bool followPlayerAgain = false;

	public float activityRadius = 15.0f;

	// Use this for initialization
	void Start () {

		// add this to a list of enemies
		am.enemies.Add(this);

		// set the spawn tile
		spawnTile = getCurTile();
		lastValidTile = spawnTile;

		// initialize this enemies current state
		initState();
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

			case AIStates.TurtleState:
				Debug.Log("--- AI STATE CHANGE: TurtleState ---");
				stateClass = new AIState(new TurtleState(this));
				break;

			case AIStates.ReturnSpawn:
				Debug.Log("--- AI STATE CHANGE: ReturnSpawn ---");
				stateClass = new AIState(new ReturnSpawn(this));
				break;

			case AIStates.ReturnSpawnWater:
				Debug.Log("--- AI STATE CHANGE: ReturnSpawnWater ---");
				stateClass = new AIState(new ReturnSpawnWater(this));
				break;

			case AIStates.RandomMovement:
				Debug.Log("--- AI STATE CHANGE: RandomMovement ---");
				stateClass = new AIState(new RandomMovement(this));
				break;

			case AIStates.FollowWaypoints:
				Debug.Log("--- AI STATE CHANGE: FollowWaypoints ---");

				// ensure at least two waypoints exist
				if (waypoints.Count > 1)
					stateClass = new AIState(new FollowWaypoints(this));
				else 
					setState(AIStates.Idle);
				break;

			case AIStates.Idle:
				Debug.Log("--- AI STATE CHANGE: Idle ---");
				stateClass = new AIState(new Idle(this));
				break; 

			default:
				Debug.Log("--- DEFAULT AI STATE CHANGE: FollowPlayer ---");
				stateClass = new AIState(new Idle(this));
				break;
		}
	}

	public Tile getCurTile(){
		RaycastHit hit;

		if(Physics.Raycast(transform.position, Vector3.down, out hit)){
			Tile cur = hit.collider.gameObject.GetComponent<Tile>();

			if (cur != null) {
				lastValidTile = cur;
				return cur;
			}
		}

		return lastValidTile;
	}

	public bool NeedToRecalculatePath(List<Node> path, int currentNode) {
		return path.Count != 0 && currentNode + 1 <= path.Count - 1 && 
			!path [currentNode + 1].tile.navigatable && lm.getPlayer().getCurTile() != path [currentNode + 1].tile;
	}

	public int getCurTileID(){
		return this.getCurTile() == null ? lastValidTile.id : this.getCurTile().id; 
	}

	public Tile getSpawnTile() {
		return spawnTile;
	}

	public void GetHitByElement(ElementType elementType) {
		switch (elementType) {
		case ElementType.Fire:
			if (isShrunk) {
				// DESTROY
			} else if (isGrown) {
				// SHRINK TO NORMAL SIZE PREFAB
				Debug.Log("ENEMY SHRUNK TO NORMAL SIZE");
				isGrown = false;
			} else {
				// SHRINK ENEMY TO SMALL PREFAB
				Debug.Log("ENEMY SHRUNK TO SMALL SIZE");
				isShrunk = true;
				setState(AIStates.WaterSearch);
			}
			break;

		case ElementType.Water:
			if (isShrunk) {
				// GROW TO NORMAL SIZE PREFAB
				Debug.Log("ENEMY GREW TO NORMAL SIZE");
				isShrunk = false;
			} else if (!isGrown) {
				// GROW TO LARGE SIZE PREFAB
				Debug.Log("ENEMY GREW TO LARGE SIZE");
				isGrown = true;
			}
			setState(AIStates.FollowPlayer);
			break;
		default:
			break;
		}
	}
	
}
