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
	public void Update () {
		if (Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.getSpawnTile().transform.position)) <= enemy.activityRadius) {
			this.enemy.setState (AIStates.FollowPlayer);
		}
	}


	// do nothing
	public void playerOnNewTile () { }

}
