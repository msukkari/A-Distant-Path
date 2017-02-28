using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrunkEnemy : MonoBehaviour {

	public float moveSpeed = 3.0f;
	public float waitTimeToGrow = 3.0f;
	public ShrunkEnemyState currentState;

	public AStar waterSearcher;

	private Player player;
	private Tile initialPosition;

	private List<Node> path;
	private int currentNode;
	private float waitingSinceSeconds;

	private bool stateChanged;
	private bool foundWater;

	void Start() {
		currentState = ShrunkEnemyState.WaterSearch;
		waterSearcher = GetComponentInChildren<AStar> ();
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player>();
		path = new List<Node> ();
		path.Add (new Node (getCurTile (), null, 0, 0));
		currentNode = 1;
		stateChanged = true;
		foundWater = true;
		initialPosition = getCurTile ();
	}

	// Update is called once per frame
	void Update () {
		if (NeedToRecalculatePath()) {
			stateChanged = true;
		}

		switch (currentState) {
		case ShrunkEnemyState.WaterSearch:
			WaterSearchBehaviour ();
			break;

		// Went to the tile and found/not found a result
		case ShrunkEnemyState.ReturnSpawn:
			ReturnSpawnBehaviour ();
			break;
		default:
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

	public void FindClosestWaterPath() {
		path = waterSearcher.AStarPath (getCurTile (), LevelManager.instance.GetClosestTileOfType (ElementType.Water, getCurTile ().transform.position));
		path.Reverse();
		currentNode = 0;

		/*for (int i = 0; i < path.Count; i++) {
			Debug.Log ("Path[" + i + "] = " + path [i].tile.name);
		}*/
	}

	public void FindPathToInitialPos() {
		path = waterSearcher.AStarPath (getCurTile (), initialPosition);
		path.Reverse();
		currentNode = 0;
	}

	public bool NeedToRecalculatePath() {
		return path.Count != 0 && currentNode + 1 <= path.Count - 1 && 
			!path [currentNode + 1].tile.navigatable && player.getCurTile() != path [currentNode + 1].tile;
	}

	public void WaterSearchBehaviour() {
		// Changed state in to the search state OR first initialized
		if (stateChanged) {
			// if not init, then dont do this until position is very close to path[currentNode]
			FindClosestWaterPath ();
			stateChanged = false;
			waitingSinceSeconds = 0.0f;
		}

		// If A* returned a valid water tile
		if (waterSearcher.found) {
			// If targeted tile somehow lost an element before it was reached, or if reched target
			if (!path [path.Count - 1].tile.HasElementOfType (ElementType.Water) || getCurTile () == path [path.Count - 2].tile) {
				// If reached target with the element still present, set foundWater flag, remove element and return to initial point
				if (path [path.Count - 1].tile.HasElementOfType (ElementType.Water)) {
					foundWater = true;
					path [path.Count - 1].tile.LoseElement ();
					currentNode = 1;
					currentState = ShrunkEnemyState.ReturnSpawn;
					path.Reverse ();
					// If no water was found, search for the next closest water tile
				}

				stateChanged = true;
				// If not reached target, incrimentally move upwards on the determined A* path list
			} else {
				this.transform.position += (path [currentNode + 1].tile.transform.position - (this.transform.position - Vector3.up)).normalized
					* moveSpeed * Time.deltaTime;
				// When closer to the next tile on list by a certain threshold, increment the current index
				if(Vector3.Distance(path [currentNode + 1].tile.transform.position + Vector3.up, this.transform.position) < 0.1f) {
					currentNode++;
				}
			}
			// If no water tile is present in the map, keep checking until something is found
		} else {
			stateChanged = true;
		}
	}

	public void ReturnSpawnBehaviour() {
		// A* back to initial position
		if (stateChanged) {
			FindPathToInitialPos ();
			stateChanged = false;
			waitingSinceSeconds = 0.0f;
		}

		// Move towards each next tile on path
		if (path.Count != 0 && getCurTile () != path [path.Count - 1].tile) {
			this.transform.position += (path [currentNode + 1].tile.transform.position - (this.transform.position - Vector3.up)).normalized
				* moveSpeed * Time.deltaTime;

			//if (getCurTile () == path [currentNode + 1].tile) {
			if (Vector3.Distance (path [currentNode + 1].tile.transform.position + Vector3.up, this.transform.position) < 0.1f) {
				currentNode++;
			}
		// REACHED TO INITIAL POSITION. EITHER WITH OR WITHOUT WATER
		} else {
			Debug.Log ("REached back to the starting position");
			// 
			if (foundWater) {
				waitingSinceSeconds += Time.deltaTime;

				if (waitingSinceSeconds >= waitTimeToGrow) {
					// GROW TO NORMAL SIZED ENEMY
					Debug.Log("Would have grown into a normal sized enemy here");
				}
			} else {
				currentState = ShrunkEnemyState.WaterSearch;
				stateChanged = true;
			}
		}
	}
}

public enum ShrunkEnemyState {
	WaterSearch = 0,
	ReturnSpawn
}
