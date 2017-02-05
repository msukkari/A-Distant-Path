using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;


public enum TimeStates {
		Past,
		Present
};

public class LevelManager : MonoBehaviour {

	// static instance of LevelManager
	public static LevelManager instance = null;


	private TimeStates TimeState;

	TimeStates getTimeState(){
		return this.TimeState;
	}

	public void setTimeState(TimeStates state){this.TimeState = state;}


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

	public void LoadLevelScene(){
		
		GameObject ETManager = PhotonNetwork.Instantiate("EventTransferManager", Vector3.zero, Quaternion.identity, 0);
		DontDestroyOnLoad(ETManager);
		

		if(TimeState == TimeStates.Past){
			SceneManager.LoadScene((int)Scenes.Past);
		}
		else{
			SceneManager.LoadScene((int)Scenes.Present);
		}
	}
 


	
	// Update is called once per frame
	void Update () { }
}
