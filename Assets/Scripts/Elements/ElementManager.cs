using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementManager : MonoBehaviour {

	public GameObject firePrefab;
	public GameObject waterPrefab;
	public GameObject transferPrefab;

	//private Dictionary<ElementType, GameObject> elementSpawnDictionary = new Dictionary<ElementType, GameObject>();
	public static Dictionary<ElementType, GameObject> elementSpawnDictionary = new Dictionary<ElementType, GameObject>();

	// Use this for initialization
	void Start () {
		elementSpawnDictionary.Add (ElementType.Fire, firePrefab);
		elementSpawnDictionary.Add (ElementType.Water, waterPrefab);
		elementSpawnDictionary.Add (ElementType.Transfer, transferPrefab);

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public GameObject GetElementOfType(ElementType elementType) {
		return elementSpawnDictionary [elementType];
	}

}
