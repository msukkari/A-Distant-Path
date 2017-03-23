using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorpseTrigger : MonoBehaviour {
	public Vector3 SpawnPoint;
	public GameObject scoutPrefab;

	// Use this for initialization
	void Start () {
		this.SpawnPoint = new Vector3(this.transform.position.x, this.transform.position.y + 10, this.transform.position.z - 1);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void OnTriggerEnter(Collider other){
		Debug.Log("TRIGGER ON CHECKPOINT");

		Player player = other.gameObject.GetComponent<Player>();

		if(player != null){
			GameObject deadScount = Instantiate(scoutPrefab, SpawnPoint, Quaternion.identity) as GameObject;			
		}

	}
}
