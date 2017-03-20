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

	public TurtleState(Enemy enemy) {
		// Set enemy instance
		this.enemy = enemy;
		this.enemy.isShrunk = true;

		// Create new A* pathfinding class
		this.star = new AStar();

		path = new List<Node> ();
		path.Add (new Node (enemy.getCurTile (), null, 0, 0));
		currentNode = 1;


	}

	public void FindClosestFoodSource() {
		
	}

	
	// Update is called once per frame
	public void Update () {
		
	}

	public void playerOnNewTile() { }
	
	public void onMetalCubeRust() {
		Debug.Log("HERHEHREHREHRHERHEHRHERHERHEHREHRHERHER");

	}
}
