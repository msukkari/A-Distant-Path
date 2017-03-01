using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnSpawn : AIStateInterface {


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



	public ReturnSpawn(Enemy enemy) {

		// set enemy class
		this.enemy = enemy;

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
				
			/*
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
		}*/
		
		}
	}

	// Unused
	public void playerOnNewTile() { }
}
