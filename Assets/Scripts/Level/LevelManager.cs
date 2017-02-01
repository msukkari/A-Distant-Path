using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	// static instance of LevelManager
	public static LevelManager instance = null;


	void Awake() {
		// if the static class instance is null (singleton pattern)
		if (instance == null)
			instance = this;

		// if instance already exists and it's not this:
		else if (instance != this)

			// then destroy this. Enforces singletonPattern
			Destroy(gameObject);

		// Sets this to not be destroyed on scene reload
		DontDestroyOnLoad(gameObject);

		Debug.Log("level initialization...");

	}	
 


	
	// Update is called once per frame
	void Update () { }
}
