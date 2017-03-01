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
	private List<Node> path = null;

	// Start and ending tiles
	private Tile start, end;

	private Vector3 target;

	public PlayerFollow(Enemy enemy) {
		this.enemy = enemy;

		// Create new A* pathfinding class
		this.star = new AStar();

		// error insurance
		start = end = enemy.getCurTile();
	}


	private void calculatePath() {
		start = enemy.getCurTile();
		path = star.AStarPath(start, end);
		path.Reverse();

		target = path[0].tile.transform.position;
		target.y = target.y + 1;
	}


	public void Update() {
			
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

		}
	}


	public void playerOnNewTile () {
		this.end = lm.player.getCurTile();
		calculatePath(); 
	}
}
