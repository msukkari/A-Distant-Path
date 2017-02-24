using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

	public GameObject firePrefab;
	public GameObject waterPrefab;
	public GameObject steamPrefab;
	public GameObject transferPrefab;
	public GameObject metalcubePrefab;
	public GameObject metalcuberustedPrefab;
	public GameObject stumpPrefab;
	public GameObject saplingPrefab;
	public GameObject bigtreePrefab;

	public static Dictionary<ElementType, GameObject> elementSpawnDictionary = new Dictionary<ElementType, GameObject>();
	public static Dictionary<HashSet<ElementType>, ElementType> elementCombinationsDictionary = 
		new Dictionary<HashSet<ElementType>, ElementType>(new HashSetEqualityComparer<ElementType>());

	// Use this for initialization
	void Start () {
		FillElementSpawnDictionary ();
		FillElementCombinationsDictionary ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	#region Public Get Methods

	public static GameObject GetElementOfType(ElementType elementType) {
		return elementSpawnDictionary [elementType];
	}

	public static ElementType GetCombinationElement(params ElementType[] elementTypes) {
		Debug.Log ("elementtypes has " + elementTypes.Length + " values");
		if (elementTypes.Length == 1) {
			return elementTypes [0];
		} else {
			HashSet<ElementType> combinationSet = GetSetFor (elementTypes);
			return (elementCombinationsDictionary.ContainsKey(combinationSet)) ? 
				elementCombinationsDictionary [GetSetFor (elementTypes)] : elementTypes [0];
		}
	}

	#endregion


	#region Element Dictionary Filler Methods

	private void FillElementSpawnDictionary() {
		elementSpawnDictionary.Add (ElementType.Fire, firePrefab);
		elementSpawnDictionary.Add (ElementType.Water, waterPrefab);
		elementSpawnDictionary.Add (ElementType.Steam, steamPrefab);
		elementSpawnDictionary.Add (ElementType.Transfer, transferPrefab);
		elementSpawnDictionary.Add (ElementType.MetalCube, metalcubePrefab);
		elementSpawnDictionary.Add (ElementType.MetalCubeRusted, metalcuberustedPrefab);
		elementSpawnDictionary.Add (ElementType.Stump, stumpPrefab);
		elementSpawnDictionary.Add (ElementType.Sapling, saplingPrefab);
		elementSpawnDictionary.Add (ElementType.BigTree, bigtreePrefab);

	}

	private void FillElementCombinationsDictionary() {
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Fire), ElementType.Fire);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Water), ElementType.Water);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Steam), ElementType.Steam);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MetalCube), ElementType.MetalCube);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MetalCubeRusted), ElementType.MetalCubeRusted);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Stump), ElementType.Stump);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Sapling), ElementType.Sapling);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.BigTree), ElementType.BigTree);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Fire, ElementType.Water), ElementType.Steam);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MetalCube, ElementType.Water), ElementType.MetalCubeRusted);	
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Stump, ElementType.Water), ElementType.Sapling);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.BigTree, ElementType.Stump), ElementType.BigTree);						
	}

	#endregion


	#region Set Methods

	private static HashSet<ElementType> GetSetFor(params ElementType[] types) {
		HashSet<ElementType> combinationSet = new HashSet<ElementType> ();

		foreach (ElementType element in types) {
			combinationSet.Add (element);
		}
		return combinationSet;
	}

	#endregion

}
