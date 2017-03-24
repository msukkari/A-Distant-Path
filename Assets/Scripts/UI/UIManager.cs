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
        foreach (var image in fire)
        {
            SetAlpha(image, 0);
        }
        foreach (var image in water)
        {
            SetAlpha(image, 0);
        }

        SetAlpha(firePlus, 0);
        SetAlpha(waterPlus, 0);
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
                        SetAlpha(fire[i], fireTransp);
                    }
                    for(; i < 5; i++)
                    {
                        SetAlpha(fire[i], 0);
                    }
                    if(entry.Value > 5)
                    {
                        SetAlpha(firePlus, fireTransp);
                    } else {
                        SetAlpha(firePlus, 0);
                    }
                }
                
                if (entry.Key == ElementType.Water) {
                    int i = 0;
                    for (; i < entry.Value; i++) {
                        if(i > 4) {
                            break;
                        }
                        SetAlpha(water[i], waterTransp);
                    }
                    for (; i < 5; i++)
                    {
                        SetAlpha(water[i], 0);
                    }
                    if (entry.Value > 5)
                    {
                        SetAlpha(waterPlus, waterTransp);
                    } else {
                        SetAlpha(waterPlus, 0);
                    }
                }
            }
        }
	}

    private void SetAlpha(Image image, float alpha)
    {
        Color temp = image.color;
        temp.a = alpha;
        image.color = temp;
    }
}
