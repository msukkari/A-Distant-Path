using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWaypoints : AIStateInterface {

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

	// Current waypoint refrence
	private int waypointIndex = 0; 

	// current waypoint
	private Tile currentWaypoint;

	// current node
	int currentNode;

	// Start and ending tiles
	private Tile start, end;


	public FollowWaypoints(Enemy enemy) {
		this.enemy = enemy;

		// Create new A* pathfinding class
		this.star = new AStar();

		path = new List<Node> ();
		path.Add (new Node (enemy.getCurTile (), null, 0, 0));

		calculatePath();
	}

	// Calculate path to next waypoint
	private void calculatePath() {

		if (waypointIndex == enemy.waypoints.Count) {
			waypointIndex = 0;
		}	

		start = enemy.getCurTile();
		end = enemy.waypoints[waypointIndex];

		path = star.AStarPath(start, end);
		Debug.Log ("Start is: " + start.name + ", end is = " + end.name);
		path.Reverse();
		currentNode = 0;
	}

	
	// Update is called once per frame
	public void Update () {

		// if a path exists
		if (path.Count != 0) {

			// check if enemy is on final tile
			if (enemy.getCurTile () != path[path.Count - 1].tile){

				enemy.transform.position += (path[currentNode + 1].tile.transform.position - (enemy.transform.position - Vector3.up)).normalized
				* enemy.moveSpeed * Time.deltaTime;

				if (Vector3.Distance (path[currentNode + 1].tile.transform.position + Vector3.up, enemy.transform.position) < 0.1f) {
					currentNode++;
				}
			
			} else {
				waypointIndex++;
				calculatePath();
			}

		}

		if (Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.getSpawnTile().transform.position)) <= enemy.activityRadius) {
			this.enemy.setState (AIStates.FollowPlayer);
		}

	}


	public void playerOnNewTile () { }
}
