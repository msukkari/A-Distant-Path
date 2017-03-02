using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : AIStateInterface {

	// AIManager instance
	private AIManager am = AIManager.instance;

	// LevelManager instance
	private LevelManager lm = LevelManager.instance;

	// Enemy class
	private Enemy enemy;

	public Idle(Enemy enemy) {

		// set enemy instance
		this.enemy = enemy;

	}
	
	// Update is called once per frame
	public void Update () { }


	// do nothing
	public void playerOnNewTile () { }

}
