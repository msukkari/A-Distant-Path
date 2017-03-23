using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure; 

public class DropTrigger : MonoBehaviour {


	public Vector3 center;
	public float radius;
	public float dropInterval;
	public float resetTime;

	private List<GameObject> objects;
	private List<GameObject> fallenObjects;
	private bool fall = false;

	private bool beenTriggered = false;

	private float now = 0.0f;

	private bool isVibrating = false;

	AudioManager au = AudioManager.instance;
	


	// Use this for initialization
	void Start () {
		this.objects = new List<GameObject>();
		this.fallenObjects = new List<GameObject>();
	}

	public void trigger() {

		if (beenTriggered) return;
		beenTriggered = true;

		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;

        foreach (Collider collider in hitColliders) {
        	GameObject obj = collider.transform.gameObject;
        	objects.Add(obj);
        }
        this.fall = true;
        au.secondary.Play();
	}

	public void OnTriggerEnter(Collider other){
		Player player = other.gameObject.GetComponent<Player>();

		// check if entered gameobject is the player
		if(player != null){
			trigger();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
		if (fall) {

			if (!isVibrating) {
				GamePad.SetVibration(0, 100.0f, 100.0f);
				isVibrating = true;
			}

			now += Time.deltaTime;

			if(objects.Count != 0) {


				if (now >= dropInterval) {

					int rand = Random.Range(0, objects.Count);

					GameObject obj = objects[rand];
					objects.Remove(obj);

					obj.AddComponent<Rigidbody>();

					fallenObjects.Add(obj);
					
					now = 0.0f;
				}


			// destroy when not needed
			} else {	

				if (isVibrating)
					GamePad.SetVibration(0, 0.0f, 0.0f);

				if (now >= resetTime) {
					foreach (GameObject obj in fallenObjects) {
						Destroy(obj);
					}
					Destroy(this);
				}
			}
		}


	}
}

