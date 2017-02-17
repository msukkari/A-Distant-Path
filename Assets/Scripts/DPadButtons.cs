using UnityEngine;
using UnityEngine;
using System.Collections;

public class DPadButtons : MonoBehaviour {
	public static bool up;
	public static bool down;
	public static bool left;
	public static bool right;

	public static bool upOnPressed;
	public static bool downOnPressed;
	public static bool leftOnPressed;
	public static bool rightOnPressed;

	private bool prevUp;
	private bool prevDown;
	private bool prevLeft;
	private bool prevRight;

	private float x;
	private float y;

	public DPadButtons() {
		up = down = left = right = false;
	}

	void Update() {
		x = Input.GetAxis ("DPadHor");
		y = Input.GetAxis ("DPadVer");

		if (x == -1) {
			left = true;
			if (prevLeft) {
				leftOnPressed = false;
			} else {
				leftOnPressed = true;
			}

			prevLeft = true;
		} else {
			left = false;
			prevLeft = false;
		}

		if (x == 1) {
			right = true;
			if (prevRight) {
				rightOnPressed = false;
			} else {
				rightOnPressed = true;
			}

			prevRight = true;
		} else {
			right = false;
			prevRight = false;
		}

		if (y == -1) {
			down = true;
			if (prevDown) {
				downOnPressed = false;
			} else {
				downOnPressed = true;
			}

			prevDown = true;
		} else {
			down = false;
			prevDown = false;
		}

		if (y == 1) {
			up = true;
			if (prevUp) {
				upOnPressed = false;
			} else {
				upOnPressed = true;
			}

			prevUp = true;
		} else {
			up = false;
			prevUp = false;
		}

	}
}