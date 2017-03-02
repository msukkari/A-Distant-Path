using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMesh : MonoBehaviour {

	List<SkinnedMeshRenderer> meshList;

	// Use this for initialization
	void Start () {
		meshList = new List<SkinnedMeshRenderer>();

		foreach(Transform child in this.transform){			
			meshList.Add(child.GetComponent<SkinnedMeshRenderer>());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void enableMesh(bool status){
		foreach(SkinnedMeshRenderer mesh in meshList){			
			mesh.enabled = status;
		}
	}
}
