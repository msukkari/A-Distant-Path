using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	public Animator anim;
	public GameObject child;
	public Collider block;

	public GameObject myFlagGO;
	public GameObject otherFlagGO;

	public Flag myFlag;
	public Flag otherFlag;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		block = child.GetComponent<BoxCollider>();

		myFlag = myFlagGO.GetComponent<Flag>();
		otherFlag = otherFlagGO.GetComponent<Flag>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
