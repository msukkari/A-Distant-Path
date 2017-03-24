using UnityEngine;
using UnityEditor;
using System.Collections;

public class ReplaceGameObjects : ScriptableWizard
{
    public bool copyValues = true;
    public GameObject NewType;
    public GameObject[] OldObjects;

    [MenuItem("Custom/Replace GameObjects")]
    static void CreateWizard()
    {
        var replaceGameObjects = ScriptableWizard.DisplayWizard<ReplaceGameObjects>("Replace GameObjects", "Replace");
        replaceGameObjects.OldObjects = Selection.gameObjects;
    }

    void OnWizardCreate()
    {
        foreach (GameObject go in OldObjects)
        {
            GameObject newObject;
            newObject = (GameObject)PrefabUtility.InstantiatePrefab(NewType);
            newObject.transform.parent = go.transform.parent;
            newObject.transform.localPosition = go.transform.localPosition;
            newObject.transform.localRotation = go.transform.localRotation;
            newObject.transform.localScale = go.transform.localScale;

            DestroyImmediate(go);
        }
    }
}