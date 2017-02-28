using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

	// A* path
	private List<Node> path;

	Vector3 target;

	// Use this for initialization
	void Start () {
		parent = transform.parent;
		this.transform.position = parent.transform.position;

		// get the parent's enemy script
		enemy = parent.GetComponent( typeof(Enemy) ) as Enemy;


		/*
		Tile end = lm.getTileList()[120];

		Debug.Log("PATH: current tile ID: " + enemy.getCurTile().id + " | target tile ID: " + end.id);


		path = findPathFromCurrentTile(end);
		path.Reverse();

		foreach (Node n in path) {
			Tile t = n.tile;
			Debug.Log(t.id);
		}

		target = path[0].tile.transform.position;
		target.y = target.y + 1;
	*/

	}

	public void test(Tile tile) {
		Debug.Log(tile.id); 

		path = findPathFromCurrentTile(tile);
		path.Reverse();

		target = path[0].tile.transform.position;
		target.y = target.y + 1;

	}

	public List<Node> findPathFromCurrentTile(Tile end) {



		// get current tile
		Tile start = enemy.getCurTile();

		return findPath(start, end);

	}


	public List<Node> findPath(Tile start, Tile end) {

		List<Node> openList = new List<Node>();
		List<Node> closedList = new List<Node>();

		Node current = new Node(start, null, 0, start.getDistance(end));
		openList.Add(current);

		// if the enemy is already at the end, return openList
		if (start == end) {
			return openList;
		}

		// While there are still nodes within the open list
		while (openList.Count > 0) {

			// Sorts openList from lowest fCost to highest
			openList = openList.OrderBy(o=>o.fCost).ToList();
			current = openList[0];

			// if the current tile is the goal
			if (current.tile == end) {
				List<Node> path = new List<Node>();

				// loop backwards through linked list of Nodes
				while (current.parent != null) {
					path.Add(current);

					// set current to parent
					current = current.parent;
				}

				// clear the lists and return path
				openList.Clear();
				closedList.Clear();
				return path;
			}


			openList.Remove(current);
			closedList.Add(current);

			// get the current tile's neighbors
			List<Tile> neighbors = current.tile.neighbors;

			foreach (Tile neighbor in neighbors) {

				if (!neighbor.navigatable) continue;

				double gCost = current.gCost + current.tile.getDistance(neighbor);
				double hCost = neighbor.getDistance(end);

				Node node = new Node(neighbor, current, gCost, hCost);

				if (tileInList(closedList, neighbor) && gCost >= node.gCost) continue;

				if (!tileInList(openList, neighbor) || gCost < node.gCost) openList.Add(node);
			}


		} 

		// Clear closed list
		closedList.Clear();
		return null;

	}

	// Check if a given tile is in List
	private bool tileInList(List<Node> list, Tile tile) {
		foreach (Node n in list) {

			if (n.tile == tile) return true;

		}

		return false;
	}

	private int speed = 2;
	// Update is called once per frame
	void Update () {



		/*
		if (path.Count != 0) {		

			if (path[0].tile != enemy.getCurTile()) {
				parent.transform.position = Vector3.MoveTowards(parent.transform.position, target, 2 * Time.deltaTime);

			// next tile
			} else {
				Debug.Log("here!!");
				path.RemoveAt(0);

				if (path.Count != 0) {
					target = path[0].tile.transform.position;
					target.y = target.y + 1;

				}
			}

		}
	*/
		
	}
}
