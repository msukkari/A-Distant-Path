using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMovement : AIStateInterface {
	
	// Random 
	private Random rand = new Random();

	// If the enemy is currently occupied
	bool occupied = false;

	// Enemy class
	private Enemy enemy;

	enum State {
		Wait,
		Walk
	}

	public RandomMovement(Enemy enemy) {
		this.enemy = enemy; 
	}


	float now = 0;
	float timeInterval = 0;
	int select;
	Vector3 direction = Vector3.forward;
	public void Update() {

		// increment now time
		now += Time.deltaTime;

		if (now >= timeInterval)
			occupied = false;


		// If the enemy is not occupied
		if (!occupied) {

			// Select a random behavior
			select = Random.Range(0, 2);
	
			// Generate a random time interval
			timeInterval = Random.Range(1, 3);

			// generate random direction
			direction = new Vector3(Random.value, 0, Random.value);
			direction.Normalize();

			Debug.Log("HERE");

			// reset time
			now = 0;

			occupied = true;
		}



		switch (select) {
			case (int) State.Wait:
				// Do nothing
				break;

			case (int) State.Walk:
				enemy.transform.Translate(direction * Time.deltaTime * 2);
				break;

			default:
				Debug.Log("something fucked up..");
				break;

		}


	}

	// do nothing..
	public void playerOnNewTile () { }

}
