using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CorpseTrigger : MonoBehaviour {
	public Vector3 SpawnPoint;
	public GameObject scoutPrefab;

	public bool hasBeenTriggered;
	public Text title;

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

		if(player != null && !hasBeenTriggered){
			GameObject deadScount = Instantiate(scoutPrefab, SpawnPoint, Quaternion.Euler(-90, 0, 0)) as GameObject;	
			this.hasBeenTriggered = true;		
		}

	}


	IEnumerator ChangeText() {
        yield return new WaitForSeconds(5);
        title.enabled = true;
    }
}
