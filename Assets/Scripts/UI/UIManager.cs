using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public List<Image> fire;
    public List<Image> water;

    public Image firePlus;
    public Image waterPlus;

    public float fireTransp;
    public float waterTransp;

    public Player player;
    public PlayerControls playerControls;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
        playerControls = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControls>();
        foreach (var image in fire) {
            image.CrossFadeAlpha(0, 0, false);
        }
        foreach (var image in water) {
            image.CrossFadeAlpha(0, 0, false);
        }

        firePlus.CrossFadeAlpha(0, 0, false);
        waterPlus.CrossFadeAlpha(0, 0, false);

    }

    // Update is called once per frame
    void Update () {

        if(playerControls.getCurrentAmmo() == 1) {
            waterTransp = 1f;
            fireTransp = 0.3f;
        } else {
            waterTransp = 0.3f;
            fireTransp = 1f;
        }
        
        if(fire != null && water != null) {
            foreach (var entry in player.elementsInventory) {
                if(entry.Key == ElementType.Fire) {
                    int i = 0;
                    for (; i < entry.Value; i++) {
                        if(i > 4) {
                            break;
                        }
                        fire[i].CrossFadeAlpha(fireTransp, 0, false);
                    }
                    for(; i < 5; i++) {
                        fire[i].CrossFadeAlpha(0, 0, false);
                    }
                    if(entry.Value > 5) {
                        firePlus.CrossFadeAlpha(fireTransp, 0, false);
                    } else {
                        firePlus.CrossFadeAlpha(0, 0, false);
                    }
                }
                
                if (entry.Key == ElementType.Water) {
                    int i = 0;
                    for (; i < entry.Value; i++) {
                        if(i > 4) {
                            break;
                        }
                        water[i].CrossFadeAlpha(waterTransp, 0, false);
                    }
                    for (; i < 5; i++) {
                        water[i].CrossFadeAlpha(0, 0, false);
                    }
                    if (entry.Value > 5) {
                        waterPlus.CrossFadeAlpha(waterTransp, 0, false);
                    } else {
                        waterPlus.CrossFadeAlpha(0, 0, false);
                    }
                }
            }
        }
	}
}
