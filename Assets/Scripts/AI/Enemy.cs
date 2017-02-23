using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {


	public GameObject behavior;


	// Use this for initialization
	void Start () {

		GameObject behaviorGO = Instantiate (behavior) as GameObject;
		behaviorGO.transform.parent = this.gameObject.transform;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
