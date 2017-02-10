using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text fireText;
	public Text waterText;

	public Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (var entry in player.elementsInventory) {
			if (entry.Key == ElementType.Fire) {
				fireText.text = "Fire Element = " + entry.Value;
			}
			if (entry.Key == ElementType.Water) {
				waterText.text = "Water Element = " + entry.Value;
			}
		}
	}
}
