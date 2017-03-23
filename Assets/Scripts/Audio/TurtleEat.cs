using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurtleEat : MonoBehaviour {


	private AudioSource audio;
	private Enemy enemy;

	public float timeInterval;
	private float now;
	private int eatCount = 0;

	// Use this for initialization
	void Start () {
		this.audio = GetComponent<AudioSource>();
		this.enemy = GetComponent<Enemy>();

		this.now = timeInterval;
	}
	
	// Update is called once per frame
	void Update () {

		if (eatCount >= 3)
			return;

		if (enemy.isEating) {

			now += Time.deltaTime;

			// if the timeInterval has been reached
			if (now >= timeInterval) {
				audio.Play();
				now = 0.0f;
				eatCount++;
			}

		}

	}
}
