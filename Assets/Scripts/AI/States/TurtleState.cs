using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleState : AIStateInterface {

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

	private bool foodFound = false;
	private Tile foodTile;

	// random movement vars
	private bool occupied = false;
	private Tile targetTile;
	private float now = 0.0f;
	private float timeInterval = 0.0f;

	private int select;
	private Vector3 direction = Vector3.forward;

	enum State {
		Wait,
		Walk
	}

	public TurtleState(Enemy enemy) {
		// Set enemy instance
		this.enemy = enemy;
		this.enemy.isShrunk = true;

		// Create new A* pathfinding class
		this.star = new AStar();

		path = new List<Node> ();
		path.Add (new Node (enemy.getCurTile (), null, 0, 0));
		currentNode = 1;

		FindClosestFoodSource();
	}

	public void FindClosestFoodSource() {

		Tile closestFood = lm.GetClosestTileOfType(ElementType.Lettuce, enemy.getCurTile().transform.position);
		if (closestFood == null) return;

		path = star.AStarPath(enemy.getCurTile(), closestFood);

		// a path to food is found!!
		if (path != null) {
			path.Reverse();
			this.foodTile = closestFood;
			foodFound = true;
			Debug.Log("PATH FOUND");
			Debug.Log(path.Count);
		} else {
			foodFound = false;
		}
	}

	
	// Update is called once per frame
	public void Update () {
		
		if (!foodFound) {
			randomMovement();
		} else {
			goToFood();
		}
	}

	private void randomMovement() {
		now += Time.deltaTime;

		if(select == 0 && now >= timeInterval) {
			occupied = false;
		}

		if (select == 1 && Vector3.Distance (targetTile.transform.position + Vector3.up, enemy.transform.position) < 0.1f) {
			occupied = false;
		}

		// If the enemy is not occupied
		if (!occupied) {
			// Select a random behavior
			select = Random.Range(0, 2);

			if (select == 1) {
				// generate random direction
				int randomVal = Random.Range (0, enemy.getCurTile ().neighbors.Count);
				targetTile = enemy.getCurTile ().neighbors [randomVal];
				
				// if the target is not navigatable, break out of this itteration
				if (!targetTile.navigatable)
					return;

				direction = targetTile.transform.position - enemy.getCurTile ().transform.position;
				direction.Normalize ();
			} else {
				timeInterval = Random.Range (0.0f, 3.0f);
				now = 0.0f;
			}


			occupied = true;
		}

		switch (select) {
			case (int) State.Wait:
				// Do nothing
				break;

			case (int) State.Walk:
			enemy.transform.position += (targetTile.transform.position - (enemy.transform.position - Vector3.up)).normalized
				* enemy.moveSpeed * Time.deltaTime / 2.0f;
				break;

			default:
				Debug.Log("something fucked up..");
				break;

		}

	}



	private void goToFood() {
		if (enemy.NeedToRecalculatePath(path, currentNode)) {
			FindClosestFoodSource();
		}
		Debug.Log("going to path..");

		// Move towards each next tile on path
		if (path.Count != 0 && enemy.getCurTile () != path [path.Count - 1].tile) {
			enemy.transform.position += (path [currentNode + 1].tile.transform.position - (enemy.transform.position - Vector3.up)).normalized
				* enemy.moveSpeed * Time.deltaTime;

			// If at the last node in path, increment the index to move to (currentNode)
			if (Vector3.Distance (path [currentNode + 1].tile.transform.position + Vector3.up, enemy.transform.position) < 0.1f) {
				currentNode++;
			}

		// Reached back to spawn tile
		}

	} 


	public void playerOnNewTile() { }
		
	public void onMetalCubeRust() {
		Debug.Log("TurtleState.cs: Metal cube has been rusted. Re-calculating path to food source");
		FindClosestFoodSource();

	}
}
