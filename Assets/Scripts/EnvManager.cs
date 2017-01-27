using UnityEngine;
using System;
using System.Collections.Generic;

public class EnvManager : MonoBehaviour {


	public Level Past;
	public Level Present;
	public Player player;


	public CameraManager cameraManager;

	public const float LEVEL_DISTANCE = 40.0f;
	public const int GRID_SIZE = 15;

	public List<GameObject> StubList;
	public List<GameObject> TreeList;


	// Use this for initialization
	void Start () {
		Cursor.visible = false;

	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown("space"))
			changeEnv();
		else if(Input.GetKeyDown(KeyCode.R))
			timeSwitch();
		else if(Input.GetKeyDown(KeyCode.E))
			plantSeed();
			
	}

	public void changeEnv(){

		if(isPlayerPast()){
			Vector3 playerPos = player.transform.position;
			int index = (GRID_SIZE * (int)Math.Round(playerPos.x, 0, MidpointRounding.AwayFromZero)) + (int)Math.Round(playerPos.z, 0, MidpointRounding.AwayFromZero);
			
			if(index < 0 || index >= Present.getTileList().Count)
				index = 0;

			Tile present = Present.getTile(index);
			Tile past = Past.getTile(index);
			present.changeColor("blue");
			past.changeColor("red");
		}

	}

	
	public void timeSwitch(){
		if(player.curTime == (int)Player.Time.PAST){
			player.transform.position = player.transform.position + new Vector3(LEVEL_DISTANCE, 0, 0);
			player.curTime = (int)Player.Time.PRESENT;
			cameraManager.changeCamera(player);
		}
		else{
			player.transform.position = player.transform.position - new Vector3(LEVEL_DISTANCE, 0, 0);
			player.curTime = (int)Player.Time.PAST;
			cameraManager.changeCamera(player);
		}
	}
	

	Boolean isPlayerPast(){
		return player.curTime == (int)Player.Time.PAST;
	}

	public void plantSeed(){
		if(isPlayerPast() && (player.getSeedCount() != 0)){
			Vector3 playerPos = player.transform.position;

			Vector3 forward = player.transform.forward;
			int angle2Right = (int)Math.Abs(Vector3.Angle(forward, Vector3.right));
			int angle2Bottom = (int)Math.Abs(Vector3.Angle(forward, -Vector3.forward));
			int angle2Left = (int)Math.Abs(Vector3.Angle(forward, -Vector3.right));
			int angle2Forward = (int)Math.Abs(Vector3.Angle(forward, Vector3.forward));

			int minAngle = Math.Min(Math.Min(angle2Left, angle2Right), Math.Min(angle2Bottom, angle2Forward));

			int index = (GRID_SIZE * (int)Math.Round(playerPos.x, 0, MidpointRounding.AwayFromZero)) + (int)Math.Round(playerPos.z, 0, MidpointRounding.AwayFromZero); 
			
			
			if(minAngle == angle2Right){
				index -= GRID_SIZE;
				print("right");
			}
			else if(minAngle == angle2Bottom){
				index++;
				print("bottom");
			}
			else if(minAngle == angle2Left){
				index += GRID_SIZE;
				print("left");
			}
			else if(minAngle == angle2Forward){
				index--;
				print("forward");
			}



			Tile selectedTile = Past.getTile(index);
			Tile presentTile = Present.getTile(index);
			Vector3 selectedPos = selectedTile.transform.position;
			Vector3 presentPos = presentTile.transform.position;


			if(!selectedTile.getPlantState()){
				int stubIndex = UnityEngine.Random.Range(0, StubList.Count);
				GameObject StubPrefab = StubList[stubIndex];
				GameObject TreePrefab = TreeList[stubIndex];

				GameObject Stub = Instantiate(StubPrefab, new Vector3(selectedPos.x, 2.12f, selectedPos.z), Quaternion.identity) as GameObject;
				GameObject Tree = Instantiate(TreePrefab, new Vector3(presentPos.x, 2.08f, presentPos.z), Quaternion.identity) as GameObject;
				Tree.transform.localScale *= 0.3f;
				Stub.transform.localScale *= 0.3f;
				selectedTile.setPlantState(true);
				presentTile.setPlantState(true);

				player.decCount();
			}

		}
	}
}
