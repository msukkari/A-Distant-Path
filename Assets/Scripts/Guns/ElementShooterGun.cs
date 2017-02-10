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
		if (owner.currentLocation != tile && owner.HasElement (currentElement, chargesPerHit) && (!tile.HasElement() || tile.element.elementType != currentElement)) {
			tile.GainElement (currentElement);
			owner.LoseElement (currentElement, chargesPerHit);
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
