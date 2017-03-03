using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAbsorbGun : Gun {

	public override void ShootGun(Tile tile) {
		if (tile.element != null) {
			ElementType elementObtained = SuckTileElement (tile);
			owner.GainElement (elementObtained, 1);
		}
	}

	public override void ShootGun(Tile tile, int chargesPerHit) {
		ShootGun (tile);
	}

	private ElementType SuckTileElement(Tile tile) {
		return tile.LoseElement ();
	}

	public override void AreaShot() {
		if (owner.getCurTile().bucket != null && owner.getCurTile().bucket.heldElement != null) {
			ElementType elementObtained = owner.getCurTile().bucket.LoseElement ().elementType;
			owner.GainElement (elementObtained, 1);
		}

		foreach (Tile neighbor in owner.getCurTile().neighbors) {
			if (neighbor.element != null && (neighbor.element.elementType == ElementType.Water || neighbor.element.elementType == ElementType.Fire)) {
				ElementType elementObtained = SuckTileElement (neighbor);
				owner.GainElement (elementObtained, 1);
			}
			if (neighbor.bucket != null && neighbor.bucket.heldElement != null) {
				ElementType elementObtained = neighbor.bucket.LoseElement ().elementType;
				owner.GainElement (elementObtained, 1);
			}
		}
	}

}
