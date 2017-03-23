using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transfer : Element {

	public AudioSource audio;

	// Use this for initialization
	void Start () {
		elementType = ElementType.Transfer;
		this.audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
