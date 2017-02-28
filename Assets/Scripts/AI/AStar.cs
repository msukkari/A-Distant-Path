using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AStar : MonoBehaviour {

	public Tile target;
	public bool found = false;

	public List<Node> AStarPath(Tile start, Tile end) {
		int count = 0;
		target = end;

		List<Node> openList = new List<Node>();
		List<Node> closedList = new List<Node>();

		Node current = new Node(start, null, 0, GetManhattanDistance(start, end));
		openList.Add(current);

		// if the enemy is already at the end, return openList
		if (start == end) {
			found = true;
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
				while (current != null) {
					path.Add(current);
					// set current to parent
					current = current.parent;
				}
				// clear the lists and return path
				openList.Clear();
				closedList.Clear();
				found = true;
				//Debug.Log ("Found path returning true");
				return path;
			}

			openList.Remove(current);
			closedList.Add(current);
			count++;
			// get the current tile's neighbors
			List<Tile> neighbors = current.tile.neighbors;

			foreach (Tile neighbor in neighbors) {
				//Debug.Log ("Checking " + neighbor.name);
				if (!neighbor.navigatable && neighbor != target) continue;

				double gCost = current.gCost + 1;
				double hCost = GetManhattanDistance(neighbor, target);

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

	private int GetManhattanDistance(Tile current, Tile target) {
		return (int)Mathf.Abs (target.transform.position.x - current.transform.position.x) + (int)Mathf.Abs (target.transform.position.y - current.transform.position.y);
	}

}
