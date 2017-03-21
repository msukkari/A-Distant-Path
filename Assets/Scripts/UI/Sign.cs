using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sign : MonoBehaviour {

	public Text SignText;
	public GameObject player;

	public string textOnSign;

	// Use this for initialization
	void Start () {
		this.player =  GameObject.FindGameObjectWithTag("Player"); 
	}
	
	// Update is called once per frame
	void Update () {
		float distToPlayer = Vector3.Distance(this.transform.position, player.transform.position);

		//Debug.Log("Distance to player: " + distToPlayer);

		if(distToPlayer	< 1.5f && SignText.text != textOnSign){
				SignText.text = textOnSign;
		}
		else if(distToPlayer >= 1.5f && SignText.text == textOnSign){
				SignText.text = "";
		}
	}
}
