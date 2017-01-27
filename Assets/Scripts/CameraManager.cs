using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {


	public GameObject camera;
	public Vector3 initPos;

	private Vector3 presentPos;

	// Use this for initialization
	void Start () {

		presentPos = initPos + new Vector3(EnvManager.LEVEL_DISTANCE, 0f, 0);

		camera.transform.position = initPos;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeCamera(Player player){
		int curTime = player.curTime;

		if(curTime == (int)Player.Time.PAST)
			camera.transform.position = initPos;
		else
			camera.transform.position = presentPos;
	}
}
