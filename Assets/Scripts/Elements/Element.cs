using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour {

	public ElementType elementType;
	public int quantity = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

public enum ElementType {
	Fire = 0,
	Water,
	Steam,
	Transfer,
	MetalCube,
	MetalCubeRusted,
	Stump,
	Sapling,
	BigTree
}
