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
	public GameObject icePrefab;
	public GameObject sandPrefab;
	public GameObject moltenSandPrefab;
	public GameObject glassPrefab;
	public GameObject stonePrefab;
	public GameObject woodPrefab;

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
		elementSpawnDictionary.Add (ElementType.Ice, icePrefab);
		elementSpawnDictionary.Add (ElementType.Sand, sandPrefab);
		elementSpawnDictionary.Add (ElementType.MoltenSand, moltenSandPrefab);
		elementSpawnDictionary.Add (ElementType.Glass, glassPrefab);
		elementSpawnDictionary.Add (ElementType.Stone, stonePrefab);
		elementSpawnDictionary.Add (ElementType.Wood, woodPrefab);
	}

	private void FillElementCombinationsDictionary() {
		// One-to-one mappings
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Fire), ElementType.Fire);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Water), ElementType.Water);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Steam), ElementType.Steam);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MetalCube), ElementType.MetalCube);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MetalCubeRusted), ElementType.MetalCubeRusted);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Stump), ElementType.Stump);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Sapling), ElementType.Sapling);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.BigTree), ElementType.BigTree);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Ice), ElementType.Ice);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Sand), ElementType.Sand);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MoltenSand), ElementType.MoltenSand);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Glass), ElementType.Glass);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Stone), ElementType.Stone);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Wood), ElementType.Wood);

		// Two element combinations
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Fire, ElementType.Water), ElementType.Steam);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MetalCube, ElementType.Water), ElementType.MetalCubeRusted);	
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Stump, ElementType.Water), ElementType.Sapling);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.BigTree, ElementType.Stump), ElementType.BigTree);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Ice, ElementType.Fire), ElementType.None);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Ice, ElementType.Water), ElementType.Ice);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Sand, ElementType.Fire), ElementType.MoltenSand);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.MoltenSand, ElementType.Water), ElementType.Sand);
		elementCombinationsDictionary.Add (GetSetFor (ElementType.Sapling, ElementType.Fire), ElementType.Stump);

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
