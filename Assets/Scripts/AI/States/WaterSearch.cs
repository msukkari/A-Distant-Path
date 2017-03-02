using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSearch : AIStateInterface {

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
	private float waitingSinceSeconds;

	public WaterSearch(Enemy enemy) {
		// Set enemy instance
		this.enemy = enemy;
		this.enemy.isShrunk = true;

		// Create new A* pathfinding class
		this.star = new AStar();

		path = new List<Node> ();
		path.Add (new Node (enemy.getCurTile (), null, 0, 0));
		currentNode = 1;

		// Init search
		FindClosestWaterPath();
	}

	public void FindClosestWaterPath() {
		path = star.AStarPath(enemy.getCurTile(), LevelManager.instance.GetClosestTileOfType(ElementType.Water, enemy.getCurTile().transform.position));
		path.Reverse();
		currentNode = 0;
	}

	
	// Update is called once per frame
	public void Update () {
		
		if (enemy.NeedToRecalculatePath(path, currentNode)) {
			FindClosestWaterPath();
		}

		// If A* returned a valid water tile
		if (star.found) {

			// If targeted tile somehow lost an element before it was reached, or if reched target
			if (!path [path.Count - 1].tile.HasElementOfType (ElementType.Water) || enemy.getCurTile () == path [path.Count - 2].tile) {

				// If reached target with the element still present, set foundWater flag, remove element and return to initial point
				if (path [path.Count - 1].tile.HasElementOfType (ElementType.Water)) {
					enemy.foundWater = true;
					path [path.Count - 1].tile.LoseElement ();
					currentNode = 1;

					this.enemy.setState(AIStates.ReturnSpawnWater);

					// If no water was found, search for the next closest water tile
				}
				// If not reached target, incrimentally move upwards on the determined A* path list
			} else {
				enemy.transform.position += (path [currentNode + 1].tile.transform.position - (enemy.transform.position - Vector3.up)).normalized
					* enemy.moveSpeed * Time.deltaTime;
				// When closer to the next tile on list by a certain threshold, increment the current index
				if(Vector3.Distance(path [currentNode + 1].tile.transform.position + Vector3.up, enemy.transform.position) < 0.1f) {
					currentNode++;
				}
			}
		}	
		// If no water tile is present in the map, keep checking until something is found
		else{
			// Recalculate path
			//FindClosestWaterPath();
			enemy.foundWater = false;
			this.enemy.setState(AIStates.ReturnSpawnWater);
		}
	}


	// Required
	public void playerOnNewTile() { }
}
