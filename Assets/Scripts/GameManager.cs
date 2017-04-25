using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics.Debug;
using UnityEngine.SceneManagement;
using UnityEngine;

/**
* GameManager: The game manager is is the core absolute in the game. It is instantiated
* on boot and persists regardless of what scene is loaded.
* NOTE: there is only ONE static instance of GameManger (I.E the class is a singleton)
*
* NOTE ON USAGE: Ensure this GameManger script is located in a bare-bones prefab. 
* To initialize, attach the Loader.cs script onto a absolute game object in a given
* scene (like the main camera). Then attach the GameManager prefab onto said script.
*
* NOTE ON SCENE INITIALIZATION: On initialization, GameManager will automatically load
* Scene 0 (Main Menu).
**/

// all game scenes
public enum Scenes {
		MainMenu,
		Loading,
		MainScene,
		Past,
		Present,
		Offline,
		AITest,
		LinkedLevelPast, //(from brendan: who's level is this?)
		TurtlePast,
		TurtlePresent,

};

public class GameManager : MonoBehaviour {

	// assets path
	//public static string ASSETSPATH = Application.dataPath;

	// game build
	private string BUILD = "final-test-build";

	// static instance of GameManager
	public static GameManager instance = null;
	public static int GRID_SIZE = 100;


	// --- NETWORK ---

	// NetworkManager GameObject
	public GameObject networkObject;

	// NetworkManager instance
	private NetworkManager network;

	// ---------------

	public GameObject UIManagerObject;

	// --- LEVEL ---

	// LevelManager GameObject
	public GameObject levelObject;

	// LevelManager instance
	private LevelManager lm;
	// ---------------

	// --- ELEMENTS ---

	// ElementManager GameObject
	public GameObject elementManagerObject;

	// ElementManager instance
	private ElementManager elementManager;
    // ---------------

    // --- CONTROLS ---

    //ControlManager GameObject
    public GameObject controlManager;

    //ControlManager instance
    private ControlManager controls;
    // ---------------

	// --- AI ---
	public GameObject aiManagerObject;
	private AIManager aiManager;
	// ---------------

	// --- Audio ---
	public GameObject audioManagerObject;
	private AudioManager audioManager;

	public Material skyBox;
	// ---------------

	// Awake is called before Start function
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

		// --- initialize build here ---
		//InitBuild();

		// --- initiate game here ---
		InitGame();
	}

	// InitBuild: Get the current build (based of the git branch)
	void InitBuild() {

		/*

		// Create new process instance
		Process p = new Process();
		p.StartInfo.UseShellExecute = false;
		p.StartInfo.RedirectStandardOutput = true;

		string path = ASSETSPATH + "/gethash.sh";

		p.StartInfo.FileName = path;
		p.Start();
		// Do not wait for the child process to exit before
		// reading to the end of its redirected stream.
		// p.WaitForExit();
		// Read the output stream first and then wait.
		string output = p.StandardOutput.ReadToEnd();
		p.WaitForExit();

		Debug.Log(output);
		*/
	}


	// InitGame: initializes the game scene
	void InitGame() {
		Debug.Log("initializing game...");
		Debug.Log(Scenes.MainMenu);

        InitControls();

        // Load main menu scene
        SceneManager.LoadScene((int) Scenes.MainMenu);		
	}	

	// InitNetowrk: initializes the networking (currently called on "play" button)
	public void InitNetwork() { 
				
		// Check if NetworkManager has already be instansiated
		if (NetworkManager.instance == null)	

			// instantiate the network prefab
			Instantiate(networkObject);

		// get the class instance
		network = NetworkManager.instance;
	}	

	// InitLevel: initializes the level following network initialization
	public void InitLevel(TimeStates TimeState) {
		
		// check if LevelManager is already instantiated
		if (LevelManager.instance == null)	

			// instantiate the level manager
			Instantiate(levelObject);

		// get class instance
		lm = LevelManager.instance;
		lm.setTimeState(TimeState);

		lm.LoadLevelScene();

		InitSound();
	}

	// InitAIManager: initializes that AI manager. Called from: LevelManager.cs
	public void InitAIManager() {

		if (AIManager.instance == null)

			Instantiate(aiManagerObject);

		aiManager = AIManager.instance;

	}
   
  public void InitControls() {
        if (ControlManager.instance == null) {
            Instantiate(controlManager);
        }

        controls = ControlManager.instance;

        controls.loadUtils();
    }

    public void InitSound() {
    	if (AudioManager.instance == null) {
            Instantiate(audioManagerObject);
        }

        audioManager = AudioManager.instance;
    }

    // GetBuild: return the current game build
    public string GetBuild() {
		return BUILD;
	}
	
	// Update is called once per frame
	void Update () {
		if(RenderSettings.skybox != skyBox){
			RenderSettings.skybox = skyBox;
		}
	}
}
