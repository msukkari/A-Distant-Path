using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	public Animator anim;
	public GameObject child;
    public bool gateOpen;
	public Collider block;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		block = child.GetComponent<BoxCollider>();
	}
	
	// Update is called once per frame
	void Update () {
        gateOpen = anim.GetBool("Open");
	}
}
