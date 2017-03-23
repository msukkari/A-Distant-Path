using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropTrigger : MonoBehaviour {


	public Vector3 center;
	public float radius;
	public float dropInterval;
	public float resetTime;

	private List<GameObject> objects;
	//private List<GameObject> fallenObjects;
	private bool fall = false;

	private bool beenTriggered = false;

	private float now = 0.0f;

	


	// Use this for initialization
	void Start () {
		this.objects = new List<GameObject>();
		//this.fallenObjects = new List<GameObject>();
	}

	public void trigger() {

		if (beenTriggered) return;
		beenTriggered = true;
		Debug.Log("TRIGGER");

		Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        int i = 0;

        foreach (Collider collider in hitColliders) {
        	GameObject obj = collider.transform.gameObject;
        	objects.Add(obj);
        }
        this.fall = true;
	}

	/*
	public void OnTriggerEnter(Collider other){
		Player player = other.gameObject.GetComponent<Player>();

		// check if entered gameobject is the player
		if(player != null){
			trigger();
		}
	}*/
	
	// Update is called once per frame
	void Update () {
	
		if (fall) {

			if(objects.Count != 0) {

				now += Time.deltaTime;

				if (now >= dropInterval) {

					int rand = Random.Range(0, objects.Count);

					GameObject obj = objects[rand];


					obj.AddComponent<Rigidbody>();
					Rigidbody gameObjectsRigidBody = obj.AddComponent<Rigidbody>(); // Add the rigidbody.
					gameObjectsRigidBody.mass = 5; // Set the GO's mass to 5 via the Rigidbody.

					//fallenObjects.Add(obj);
					objects.Remove(obj);

					//UnityEngineInternal.APIUpdaterRuntimeServices.AddComponent(obj, "Assets/Scripts/DropTrigger.cs (19,10)", "Rigidbody");
					//obj.AddComponent<Rigidbody>();

					//if (obj.GetComponent<Rigidbody>() == null) {
						
					//}
				}

			} 

			/*else {
				Debug.Log("here!!");
				if (now >= resetTime) {
					foreach (GameObject obj in fallenObjects) {
						Destroy(obj);
					}
					Destroy(this);
					Debug.Log("HEREHE");
				}
			}*/
		}
	}
}
