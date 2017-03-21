using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : AIStateInterface {
	// AIManager instance
	private AIManager am = AIManager.instance;

	// LevelManager instance
	private LevelManager lm = LevelManager.instance;
	
	// Random 
	private Random rand = new Random();

	// If the enemy is currently occupied
	bool occupied = false;

	// Enemy class
	private Enemy enemy;
	private Tile targetTile;

	private AStar star;

	private float now = 0.0f;
	private float timeInterval = 0.0f;

	private int select;
	private Vector3 direction = Vector3.forward;

	enum State {
		Wait,
		Walk
	}

	public RandomMovement(Enemy enemy) {
		this.enemy = enemy; 
		star = new AStar ();
		targetTile = enemy.getCurTile ();
	}

	public void Update() {
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

		if (Mathf.Abs (Vector3.Distance (lm.getPlayer ().transform.position, enemy.getSpawnTile().transform.position)) <= enemy.activityRadius) {
			this.enemy.setState (AIStates.FollowPlayer);
		}
	}

	// do nothing..
	public void playerOnNewTile () { }
	public void onMetalCubeRust() { }

}
