using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour {


	private CharacterController controller;
	private AudioSource audio;
	private Player player;

	public AudioClip[] tracks;

	// level manager instance
	private LevelManager lm = LevelManager.instance;

	private Tile curTile;


	// Use this for initialization
	void Start () {
		
		this.controller = GetComponent<CharacterController>();

		// footsteps may only be used on the player..
		if (controller == null)
			Destroy(this);

		this.player = lm.getPlayer();
		this.audio = player.audio;

		audio.clip = tracks[0];

		curTile = player.getCurTile();
		setClip();
	}

	// sets the audio clip depenedent on current material
	private void setClip() {

		Element element;
		if(curTile != null){
			element = curTile.element;
		}
		else{
			element = null;
		}

		if (element == null) { 
			audio.clip = tracks[0];
			return;
		}


		switch (element.elementType) {

			case ElementType.Water:
				audio.clip = tracks[1];
				break;

			case ElementType.Transfer:
				audio.clip = tracks[2];
				break;

			default:
				audio.clip = tracks[0];
				break;

		}


	}

	void playStep() {

		Tile temp = player.getCurTile();

		if (temp != curTile) {
			Debug.Log("tile set");
			curTile = temp;
			setClip();
		}

		if (controller.isGrounded == true && audio.isPlaying == false) {
				 
			Debug.Log("step"); 
			audio.volume = Random.Range(0.8f, 1);
			audio.pitch = Random.Range(0.95f, 1.05f);

			audio.Play();
		}

	}

	// --- triggered by animation event ---
	public void footstepOne() { playStep(); }	
	public void footstepTwo() { playStep(); }
	
	// Update is called once per frame
	void Update () { }
}
