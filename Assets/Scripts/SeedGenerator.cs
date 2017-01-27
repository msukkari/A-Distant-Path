using UnityEngine;
using System.Collections.Generic;

public class SeedGenerator : MonoBehaviour {


	public EnvManager EV;
	public GameObject SeedPrefab;
	public int MAX_NUM_SEEDS;
	public Player player;

	private List<GameObject> SeedList;
	private int curSeedCount;

	// Use this for initialization
	void Start () {
		curSeedCount = 0;

	}
	
	// Update is called once per frame
	void Update () {
	

		while(curSeedCount < MAX_NUM_SEEDS){

			int randomIndex = UnityEngine.Random.Range(0, EV.Past.getTileList().Count);
			Tile selectedTile = EV.Past.getTile(randomIndex);

			if(!selectedTile.getPlantState() && !selectedTile.getSeedState()){
				GameObject seed = Instantiate(SeedPrefab, selectedTile.transform.position + new Vector3(0f, 1f, 0f), Quaternion.identity) as GameObject;
				seed.gameObject.AddComponent<Seed>();	

				selectedTile.setSeedState(true);
				curSeedCount++;

			}
		}
	}

	public int getCurSeedCount(){return curSeedCount;}
	public void setCurSeedCount(int newCount){curSeedCount = newCount;}
	public void OnGetSeed(){
		player.OnGetSeed();
		curSeedCount--;
	}
}
