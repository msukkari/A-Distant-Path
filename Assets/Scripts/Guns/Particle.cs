using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : MonoBehaviour {
    GameObject target;
    ElementType element;
    Player owner;
    Tile tile;
    public enum Type { Absorb, Shoot};

    Type type;

    public ParticleSystem fireSystem;
    public ParticleSystem waterSystem;
       
    float speed = 5f;
	// Use this for initialization
	void Start () {
        //fireSystem.gameObject.SetActive(false);
        //waterSystem.gameObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(target != null) {

            if (type == Type.Absorb) {
                transform.position = Vector3.Lerp(transform.position, target.transform.position + new Vector3(0, 0.5f, 0), Time.deltaTime * speed);
                if ((transform.position - (target.transform.position + new Vector3(0, 0.5f, 0))).sqrMagnitude <= 0.1f) {
                    owner.GainElement(element, 1);
                    Destroy(gameObject);
                }
            } else if (type == Type.Shoot) {
                transform.position = Vector3.Lerp(transform.position, target.transform.position, Time.deltaTime * speed);
                if ((transform.position - (target.transform.position )).sqrMagnitude <= 0.1f) {
                    tile.GainElement(element);
                    //Destroy(gameObject);
                }
            }
            
        }
	}

    public void setParameters(GameObject target, ElementType element, Player owner) {
        this.type = Type.Absorb;
        if (this.target == null ||  this.owner == null) {
            this.target = target;
            this.element = element;
            this.owner = owner;
            if(element == ElementType.Fire) {
                fireSystem.gameObject.SetActive(true);
                waterSystem.gameObject.SetActive(false);
            } else if (element == ElementType.Water){
                waterSystem.gameObject.SetActive(true);
                fireSystem.gameObject.SetActive(false);
            }
        }
    }

    public void setParameters(GameObject target, Player owner, ElementType element, Tile tile) {
        this.type = Type.Absorb;
        if (this.target == null || this.owner == null) {
            this.target = target;
            this.owner = owner;
            this.tile = tile;
            this.element = element;
        }
    }
}
