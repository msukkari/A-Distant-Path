using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : AIStateInterface {

	// Enemy class
	private Enemy enemy;

	public PlayerFollow(Enemy enemy) {

		this.enemy = enemy;
	}


	public void Update() {

		enemy.transform.Translate(Vector3.forward * Time.deltaTime);
	
	}
}
