using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : MonoBehaviour {

	public ElementType elementType;
	public int quantity = 1;

	public bool navigatable = true;
	public bool climable = false;
	public bool highlightable;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public virtual bool WaterInteract(EventTransferManager ETManager) {
		return false;
	}

	public virtual bool FireInteract(EventTransferManager ETManager) {
		return false;
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
	BigTree,
	Ice,
	None,
	Sand,
	MoltenSand,
	Glass,
	Stone,
	Wood
}
