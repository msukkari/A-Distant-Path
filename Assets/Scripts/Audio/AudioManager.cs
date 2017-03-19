using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour {

	// static instance of NetworkManager
	public static AudioManager instance = null;

	// GameManager instance
	private GameManager gm = GameManager.instance;

	private AudioSource audio;

	// track list
	public AudioClip[] tracks;


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

		this.audio = GetComponent<AudioSource>();
		this.audio.clip = tracks[1];
		this.audio.Play();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
