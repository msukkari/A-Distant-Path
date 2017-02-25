using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour {
    public static ControlManager instance = null;

    public GameObject camera;

    void Awake() {
        // if the static class instance is null (singleton pattern)
        if (instance == null)
            instance = this;

        // if instance already exists and it's not this:
        else if (instance != this)

            // then destroy this. Enforces singletonPattern
            Destroy(gameObject);

        // Sets this to not be destroyed on scene reload
        DontDestroyOnLoad(gameObject);

        Debug.Log("Controls initialization...");
    }

    public void loadUtils() {

    }
}
