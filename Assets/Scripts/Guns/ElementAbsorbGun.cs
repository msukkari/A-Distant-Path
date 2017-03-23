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
		foreach (Tile neighbor in owner.getCurTile().neighbors) {
			if (neighbor.element != null && (neighbor.element.elementType == ElementType.Water || neighbor.element.elementType == ElementType.Fire)) {
					
				Debug.Log("SUCK");

				if (neighbor.element.elementType == ElementType.Fire) {
					Debug.Log("HERE");
					neighbor.element.GetComponent<AudioSource>().Stop();
				}
				ElementType elementObtained = SuckTileElement (neighbor);


                GameObject particle = Instantiate(Resources.Load("Particle")) as GameObject;
                particle.transform.position = neighbor.transform.position + new Vector3(0, 0.5f, 0);
                particle.GetComponent<Particle>().setParameters(GameObject.FindGameObjectWithTag("Player"), elementObtained, owner);
            }
		}
	}

}
