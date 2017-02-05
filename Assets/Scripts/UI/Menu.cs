using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {


	// Main canvas object
	public Canvas MainCanvas;

	// GameManager GetInstance
	private GameManager gm = GameManager.instance;


	// Called before Start()
	void Awake() {

	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	// <BUILT IN CALL> LoadOn: Called when play button is clicked
	public void LoadOn() {

		// Initialize network connection
		gm.InitNetwork();

		// Load loading scene
		SceneManager.LoadScene((int) Scenes.Loading);	
 	}
}
