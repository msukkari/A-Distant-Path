using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementShooterGun : Gun {

	public ElementType currentElement;

	void Start() {
		base.Start ();
		isChargeable = true;
	}

	public void SetAmmoElementType(ElementType elementType) {
		currentElement = elementType;
	}


	public override void ShootGun(Tile tile, int chargesPerHit) {
        Debug.Log("Shooting");
        if (owner.currentLocation != tile && owner.HasElement (currentElement, chargesPerHit) && (!tile.HasElement() || tile.element.elementType != currentElement)) {
			owner.LoseElement (currentElement, chargesPerHit);


            GameObject particle = Instantiate(Resources.Load("Particle")) as GameObject;
            particle.transform.position = GameObject.FindGameObjectWithTag("Player").transform.position;
            particle.GetComponent<Particle>().setParameters(tile.gameObject, owner, currentElement, tile);


        }
    }

	public override void ChangeMode() {
		ChangeElement ();
	}

	private void ChangeElement() {
		currentElement = (ElementType)(((int)currentElement + 1) % (Enum.GetNames (typeof(ElementType)).Length));
		Debug.Log ("Changed element to: " + currentElement);
	}

}
