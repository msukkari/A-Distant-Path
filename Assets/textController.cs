using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textController : MonoBehaviour {
    public RectTransform t;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        RectTransform t = this.GetComponentInParent<RectTransform>();
        t.anchoredPosition += Vector2.up;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
	}
}
