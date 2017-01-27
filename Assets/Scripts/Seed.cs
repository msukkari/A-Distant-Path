using UnityEngine;
using System.Collections;

public class Seed : MonoBehaviour {

	public SeedGenerator SG;
	public Player player;



	// Use this for initialization
	void Start () {
		GameObject GO = GameObject.Find("SeedGenerator");
		SG = GO.GetComponent<SeedGenerator>();

		transform.localScale *= 0.35f;
		Collider collider = transform.gameObject.AddComponent<SphereCollider>();
		Rigidbody rb = transform.gameObject.AddComponent<Rigidbody>();
		collider.isTrigger = true;
		rb.isKinematic = true;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other){
		SG.OnGetSeed();
		Destroy(gameObject);
	}
}
