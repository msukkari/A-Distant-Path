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

	private float waitTime;
	private float maxWaitTime = 6.0f;
	private float minWaitTime = 2.0f;
	private int state = 0;

	// random movement vars
	public float directionChangeInterval = 1;
	public float maxHeadingChange = 30;
	CharacterController controller;
	private float heading;
	private Vector3 targetRotation;
	private IEnumerator randomCoroutine;
	

	private enum State {
		Wait,
		Walk,
	}


	public TurtleState(Enemy enemy) {
		// Set enemy instance
		this.enemy = enemy;
		this.enemy.isShrunk = true;

		this.controller = enemy.GetComponent<CharacterController>();

		// Create new A* pathfinding class
		this.star = new AStar();

		path = new List<Node> ();
		path.Add (new Node (enemy.getCurTile (), null, 0, 0));
		currentNode = 0;

		this.waitTime = Random.Range(minWaitTime, maxWaitTime);
		this.state = (int) State.Wait;
	}

	public void FindClosestFoodSource() {

		//Tile closestFood = lm.GetClosestTileOfType(ElementType.Lettuce, enemy.getCurTile().transform.position);
			
		Tile closestFood = enemy.goal;

		if (closestFood == null) {
			Debug.Log("NO FOOD TILE PRESENT ON MAP");
			return;
		}

		Debug.Log("current: " + enemy.getCurTile().id);
		Debug.Log("target: " + closestFood.id);

		path = star.AStarPath(enemy.getCurTile(), closestFood);

		// a path to food is found!!
		if (path != null) {
			path.Reverse();
			this.foodTile =  closestFood;
			foodFound = true;
			Debug.Log("PATH FOUND");
			
			Debug.Log(" --- Length of path: " + path.Count);
			Debug.Log("Tiles ID's of path: ");
			foreach (Node node in path) {
				Debug.Log("id: " + node.tile.id);
			}

			Debug.Log("-----------");

		} else {
			Debug.Log("NO FOOD FOUND");
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


	float now = 0.0f;
	Vector3 direction;
	Quaternion lookRotation;


	Vector3 moveDirection = Vector3.zero;
	Tile target;
	private void randomMovement() {

		switch (state) {
			case (int) State.Wait:
				now += Time.deltaTime;
				controller.SimpleMove(Vector3.down);

				if (now > waitTime) {
					now = 0.0f;
					resetRandom();
				}
				break;

			case (int) State.Walk:
				direction = (target.transform.position - enemy.getCurTile().transform.position).normalized;
				direction.y = 0;
				lookRotation = Quaternion.LookRotation(direction);
				enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * enemy.rotateSpeed);

				//enemy.transform.position += direction * Time.deltaTime;
				Vector3 forward = enemy.transform.TransformDirection(Vector3.forward) * enemy.moveSpeed;
				controller.SimpleMove(forward * Time.deltaTime);

				Debug.Log(enemy.getCurTile().id);

				if (enemy.getCurTile() == target) {
					Debug.Log("TURTLE IS BEING RESET");
					resetRandom();
				}

				break;
		}


	}

	private void resetRandom() {

		// generate new state
		//state = Random.Range(0, 2);
		state = (state + 1) % 2;

		switch (state) {
			case (int) State.Wait:

				// re-set the wait time
				this.waitTime = Random.Range(minWaitTime, maxWaitTime);
				break;

			case (int) State.Walk:

				List<Tile> waypoints = enemy.waypoints;

				// if the current tile is a waypoint, remove it
				//waypoints.Remove(enemy.getCurTile());

				int rand = Random.Range(0, waypoints.Count);
				target = waypoints[rand];
				
				break;
		}

	}


	private void goToFood() {
		if (enemy.NeedToRecalculatePath(path, currentNode)) {
			FindClosestFoodSource();
		}
		Debug.Log("going to path..");

		Debug.Log("first location: " + path[0].tile.id);

		// there exists a path
		if (path.Count != 0) {

			Tile final = path [path.Count - 1].tile;
			if (enemy.getCurTile() == final) {
				Debug.Log("TARGET REACHED");
				this.enemy.isEating = true;
				return;
			}


			if (enemy.getCurTile() == path[currentNode].tile) {
				Debug.Log("ONTO NEW NODE");
				currentNode++;
				target = path[currentNode].tile;
				

			} else {

				direction = (target.transform.position - enemy.transform.position).normalized;
				direction.y = 0;
				lookRotation = Quaternion.LookRotation(direction);
				enemy.transform.rotation = Quaternion.Slerp(enemy.transform.rotation, lookRotation, Time.deltaTime * enemy.rotateSpeed);
				//enemy.transform.position += direction * Time.deltaTime;
			
				Vector3 forward = enemy.transform.TransformDirection(Vector3.forward) * enemy.moveSpeed;
				controller.SimpleMove(forward * Time.deltaTime);

			}

		}


	} 


	public void playerOnNewTile() { }
		
	public void onMetalCubeRust() {
		Debug.Log("TurtleState.cs: Metal cube has been rusted. Re-calculating path to food source");
		FindClosestFoodSource();

	}
}
