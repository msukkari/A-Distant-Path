using System.Collections;
using System.Collections.Generic;

public class Node {

	public Tile tile;
	public Node parent; 
	public double fCost, gCost, hCost;

	public Node(Tile tile, Node parent, double gCost, double hCost) {
		this.tile = tile;
		this.parent = parent;
		this.gCost = gCost;
		this.hCost = hCost;
		this.fCost = this.gCost + this.hCost;
	}
}
