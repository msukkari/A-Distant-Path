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

	private float waitingSinceSeconds = 0.0f;
	public float WAIT_FOR_SECONDS_UNTIL_ATTACK = 0.0f;

	public ReturnSpawn(Enemy enemy) {
		// set enemy class
		this.enemy = enemy;
		this.enemy.isShrunk = false;

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
			FindPathToInitialPos();
		}

		// Move towards each next tile on path
		if (path.Count != 0 && enemy.getCurTile () != path [path.Count - 1].tile) {
			enemy.transform.position += (path [currentNode + 1].tile.transform.position - (enemy.transform.position - Vector3.up)).normalized
				* enemy.moveSpeed * Time.deltaTime;

			// If at the last node in path, increment the index to move to (currentNode)
			if (Vector3.Distance (path [currentNode + 1].tile.transform.position + Vector3.up, enemy.transform.position) < 0.1f) {
				currentNode++;
			}

		// Reached back to spawn tile
		} else {
			Debug.Log ("Reached back to the starting position");
			waitingSinceSeconds += Time.deltaTime;

			if (waitingSinceSeconds >= WAIT_FOR_SECONDS_UNTIL_ATTACK) {
				lm.getPlayer ().Freeze (false);
				enemy.frozePlayer = false;

				if (Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.getSpawnTile().transform.position)) <= enemy.activityRadius) {
					enemy.followPlayerAgain = true;
					this.enemy.setState (AIStates.FollowPlayer);
				}
				else{
					this.enemy.setState (AIStates.RandomMovement);
				}
			}
		}

		if (!enemy.frozePlayer && Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.getSpawnTile().transform.position)) <= enemy.activityRadius) {
			enemy.followPlayerAgain = true;
			this.enemy.setState (AIStates.FollowPlayer);
		}
	}

	// Unused
	public void playerOnNewTile() { }
}
