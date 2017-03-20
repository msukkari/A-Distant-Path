using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	public Text fireText;
	public Text waterText;

    public List<Image> fire;
    public List<Image> water;
   


    public Player player;

	// Use this for initialization
	void Start () {
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ();
        foreach(var image in fire) {
            image.CrossFadeAlpha(0, 0, false);
        }
        foreach (var image in water) {
            image.CrossFadeAlpha(0, 0, false);
        }
    }
	
	// Update is called once per frame
	void Update () {
		/*foreach (var entry in player.elementsInventory) {
			if (entry.Key == ElementType.Fire) {
				fireText.text = "Fire Element = " + entry.Value;
			}
			if (entry.Key == ElementType.Water) {
				waterText.text = "Water Element = " + entry.Value;
			}
		}*/
        
        if(fire != null && water != null) {
            foreach (var entry in player.elementsInventory) {
                if(entry.Key == ElementType.Fire) {
                    int i = 0;
                    for (; i < entry.Value; i++) {
                        if(i > 4) {
                            break;
                        }
                        fire[i].CrossFadeAlpha(1, 0, false);
                    }
                    for(; i < 5; i++) {
                        fire[i].CrossFadeAlpha(0, 0, false);
                    }
                }
                
                if (entry.Key == ElementType.Water) {
                    int i = 0;
                    for (; i < entry.Value; i++) {
                        if(i > 4) {
                            break;
                        }
                        water[i].CrossFadeAlpha(1, 0, false);
                    }
                    for (; i < 5; i++) {
                        water[i].CrossFadeAlpha(0, 0, false);
                    }
                }
            }
        }
	}
}
