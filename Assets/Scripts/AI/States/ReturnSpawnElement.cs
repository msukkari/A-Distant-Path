using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnSpawnElement : AIStateInterface {

	// AIManager instance
	private AIManager am = AIManager.instance;

	// LevelManager instance
	private LevelManager lm = LevelManager.instance;

	// Enemy class
	private Enemy enemy;

	// A* class
	private AStar star;

	// Path
	private List<Node> path;
	private int currentNode;

	private float waitingSinceSeconds = 0.0f;
	private float waitTimeToGrow = 2.0f;

	public ReturnSpawnElement(Enemy enemy) {
		// set enemy class
		this.enemy = enemy;
		this.enemy.isShrunk = true;

		// Create new A* pathfinding class
		this.star = new AStar();

		path = new List<Node> ();
		path.Add (new Node (enemy.getCurTile (), null, 0, 0));

		// Set current node
		currentNode = 1;

		FindPathToInitialPos();
	}
	
	private void FindPathToInitialPos() {
		path = star.AStarPath(enemy.getCurTile(), enemy.getSpawnTile());
		path.Reverse();
		currentNode = 0;
	}

	// Update is called once per frame
	public void Update () {

		if (enemy.NeedToRecalculatePath(path, currentNode)) {
			waitingSinceSeconds += Time.deltaTime;

			if (waitingSinceSeconds >= waitTimeToGrow) {
				FindPathToInitialPos();
				waitingSinceSeconds = 0.0f;
			}
		}

		if (!enemy.foundElement && lm.GetClosestTileOfType (ElementType.Water, enemy.transform.position)) {
			this.enemy.setState (AIStates.ElementSearch);
		}
		
		// Move towards each next tile on path
		if (path.Count != 0 && enemy.getCurTile () != path [path.Count - 1].tile) {
			enemy.transform.position += (path [currentNode + 1].tile.transform.position - (enemy.transform.position - Vector3.up)).normalized
				* enemy.moveSpeed * Time.deltaTime;

			//if (getCurTile () == path [currentNode + 1].tile) {
			if (Vector3.Distance (path [currentNode + 1].tile.transform.position + Vector3.up, enemy.transform.position) < 0.1f) {
				currentNode++;
			}

		// REACHED TO INITIAL POSITION. EITHER WITH OR WITHOUT WATER
		} else {

			// Note: this section is commented out because we need to figure out how ALL AI reacts to reaching spawn..
			Debug.Log ("Reached back to the starting position");
			// 
			if (enemy.foundElement) {
				waitingSinceSeconds += Time.deltaTime;

				if (waitingSinceSeconds >= waitTimeToGrow) {
					// GROW TO NORMAL SIZED ENEMY
					Debug.Log("Would have grown into a normal sized enemy here");
					enemy.foundElement = false;
					if (Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.transform.position)) <= enemy.activityRadius) {
						//this.enemy.setState (AIStates.FollowPlayer);
					}
					else{
						//this.enemy.setState (this.enemy.initialState);
					}
					enemy.isShrunk = false;
					this.enemy.setState (this.enemy.initialState);
				}
			}
		}
	}

	// Unused
	public void playerOnNewTile() { }
}
