using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AStar {

	// Return the path
	public List<Tile> findPath(Tile start, Tile end) {

		List<Node> openList = new List<Node>();
		List<Node> closedList = new List<Node>();

		Node current = new Node(start, null, 0, start.getDistance(end));
		openList.Add(current);

		// if the enemy is already at the end, return openList
		if (start == end) {

			// convert nodes -> tiles
			List<Tile> path = new List<Tile>();
			path.Add(openList[0].tile);

			// return path
			return path;	
		}

		// While there are still nodes within the open list
		while (openList.Count > 0) {

			// Sorts openList from lowest fCost to highest
			openList = openList.OrderBy(o=>o.fCost).ToList();
			current = openList[0];

			// if the current tile is the goal
			if (current.tile == end) {
				List<Tile> path = new List<Tile>();

				// loop backwards through linked list of Nodes
				while (current.parent != null) {
					path.Add(current.tile);

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
	
}
