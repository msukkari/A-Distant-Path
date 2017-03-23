using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : Element {

	public int IGNITE_THRESHOLD;
	public List<Tile> nList;
	public bool fireSystemOn = false;

	public AudioSource audio;

	// Use this for initialization
	void Start () {
		elementType = ElementType.Fire;
		navigatable = false;
		nList = transform.parent.GetComponent<Tile>().neighbors;
		IGNITE_THRESHOLD = 1000;
		this.audio = GetComponent<AudioSource>();

		if(nList == null){
			Debug.Log("Error getting nList in Fire Element");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(fireSystemOn){
			stepFireSystem();
		} 
	}

	private void stepFireSystem(){
		int roll = Random.Range(0, IGNITE_THRESHOLD) + 1;

		if(roll == IGNITE_THRESHOLD){
			Debug.Log("IGNITE THRESHOLD HIT!");
			Tile hitTile = nList[Random.Range(0, nList.Count)];

			if(hitTile.element == null){
				hitTile.GainElement(ElementType.Fire);
			}
		}
	}

	public override bool WaterInteract(EventTransferManager ETManager) {
		return this.GetComponentInParent<Tile> ().GainElement (ElementType.Water);;
	}
}