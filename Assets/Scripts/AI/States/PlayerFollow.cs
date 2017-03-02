using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : AIStateInterface {

	// AIManager instance
	private AIManager am = AIManager.instance;

	// LevelManager instance
	private LevelManager lm = LevelManager.instance;

	// Enemy class
	private Enemy enemy;

	// A* class
	private AStar star;

	// Current path
	private List<Node> path;

	// Start and ending tiles
	private Tile start, end;

	private Vector3 target;

	int currentNode;

	public PlayerFollow(Enemy enemy) {
		this.enemy = enemy;
		enemy.isShrunk = false;

		// Create new A* pathfinding class
		this.star = new AStar();

		path = new List<Node> ();
		path.Add (new Node (enemy.getCurTile (), null, 0, 0));

		// Set current node
		currentNode = 1;

		// error insurance
		start = end = enemy.getCurTile();
	}


	private void calculatePath() {
		start = enemy.getCurTile();
		path = star.AStarPath(start, end);
		path.Reverse();
		currentNode = 0;

		//target = path[0].tile.transform.position;
		//target.y = target.y + 1;
	}


	public void Update() {
		if (enemy.NeedToRecalculatePath(path, currentNode)) {
			calculatePath();
		}

			
		if (path.Count != 0 && enemy.getCurTile () != path [path.Count - 1].tile) {
			enemy.transform.position += (path [currentNode + 1].tile.transform.position - (enemy.transform.position - Vector3.up)).normalized
			* enemy.moveSpeed * Time.deltaTime;

			//if (getCurTile () == path [currentNode + 1].tile) {
			if (Vector3.Distance (path [currentNode + 1].tile.transform.position + Vector3.up, enemy.transform.position) < 0.1f) {
				currentNode++;
			}

		} else if (Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.transform.position)) <= 1.0f) {
			lm.getPlayer ().Freeze (true);
			this.enemy.setState(AIStates.ReturnSpawn);
		}

		if (Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.getSpawnTile().transform.position)) > enemy.activityRadius) {
			this.enemy.setState (AIStates.ReturnSpawn);
		}
		/*
		if (path.Count != 0) {		

			if (path[0].tile != enemy.getCurTile()) {
				enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, target, 2 * Time.deltaTime);

			// next tile
			} else {
				path.RemoveAt(0);

				if (path.Count != 0) {
					target = path[0].tile.transform.position;
					target.y = target.y + 1;
				}
			}

		}*/
	}


	public void playerOnNewTile () {
		this.end = lm.getPlayer().getCurTile();
		calculatePath(); 
	}
}
