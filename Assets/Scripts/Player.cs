using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour{

	public enum Time{PAST, PRESENT};
	public int curTime;
	public GameObject gameObject;

	private int seedCount;



	void Start(){
		curTime = (int)Time.PAST;
		seedCount = 0;
	}

	public void OnGetSeed(){seedCount++;}
	public void decCount(){seedCount--;}
	public int getSeedCount(){return seedCount;}

	
}
