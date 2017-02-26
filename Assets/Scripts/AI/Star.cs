using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour {


	// Level manager instance
	private LevelManager lm = LevelManager.instance;

	// Game manager instance
	private GameManager gm = GameManager.instance;

	// Parent enemy
	private Transform parent;

	// Enemy instance
	private Enemy enemy;


	// Use this for initialization
	void Start () {
		parent = transform.parent;

		// get the parent's enemy script
		enemy = parent.GetComponent( typeof(Enemy) ) as Enemy;



		Tile tile1 = enemy.getCurTile();
		Tile tile2 = lm.getTileList()[14];

		Debug.Log("tile 1: " + tile1.id);
		Debug.Log("tile 2: " + tile2.id);

		Debug.Log("Distance: " + Vector3.Distance(tile1.transform.position, tile2.transform.position));

		Node n = new Node();

	}

	private void findPathFromCurrentTile(Tile end) {

		// get current tile
		Tile start = enemy.getCurTile();

		findPath(start, end);

	}



	private void findPath(Tile start, Tile end) {

		List<Tile> openList = new List<Tile>();
		List<Tile> closedList = new List<Tile>();






	}



	// Update is called once per frame
	void Update () {
		
	}
}
