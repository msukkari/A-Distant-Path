using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AudioManager : MonoBehaviour {

	// static instance of NetworkManager
	public static AudioManager instance = null;

	// GameManager instance
	private GameManager gm = GameManager.instance;

	//AudioSources
	public AudioSource primary;
	public AudioSource secondary;
	public AudioSource tertiary;

	// track list
	public AudioClip[] tracks;

	// fade interval
	public float fadeInterval;

	// track index
	private int index = 0;

	public bool fadeFrom1to3 = false;
	public bool isFading = false;
	private float targetVol;  
	public bool stop = false;

	// Use this for initialization
	void Start () {
		// if the static class instance is null (singleton pattern)
		if (instance == null)
			instance = this;

		// if instance already exists and it's not this:
		else if (instance != this)

			// then destroy this. Enforces singletonPattern
			Destroy(gameObject);

		// Sets this to not be destroyed on scene reload
		DontDestroyOnLoad(gameObject);

		this.primary = GetComponent<AudioSource>();
		this.primary.clip = tracks[1];
		this.primary.Play();

		this.tertiary.volume = 0;
	}
	
	// Update is called once per frame
	void Update () {
		if (stop) return;

		if (!primary.isPlaying) {
			index++;
			if (index == tracks.Length) index = 0;

			this.primary.clip = tracks[0];
			this.primary.Play();	

			return;
		}


		if (fadeFrom1to3) {

			if (!isFading) {
				isFading = true;
				this.targetVol = primary.volume;
				return;
			}	

			



		}

		

	}


}
