using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Gun : MonoBehaviour {

	public Player owner;

	public float radiusRange = 2.0f;
	public int chargesPerHit = 1;

	public bool isChargeable = false;
	public float chargeSpeed = 1.0f;

	public float chargeLevel = 0.0f;

	// Use this for initialization
	protected void Start () {
		owner = GameObject.FindGameObjectWithTag("Player").GetComponent<Player> ();
	}

	public virtual void ShootGun(Tile tile){

	}

	public virtual void ShootGun(Tile tile, int chargesPerHit){

	}

	public virtual void ChangeMode() {

	}

	public virtual void AreaShot() {

	}

	public virtual void ChargeWeapon() {

	}
}
