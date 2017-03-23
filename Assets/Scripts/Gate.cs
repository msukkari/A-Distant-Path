using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gate : MonoBehaviour {

	public Animator anim;
    public bool gateOpen;
	public Collider block;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        gateOpen = anim.GetBool("Open");
        if (block == null)
        {
            Destroy(gameObject);
        }
	}
}
