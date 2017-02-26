using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    //External Objects
    public CharacterController cc;
    public GameObject pivotPoint;
    public GameObject cursor;

    //Parameters
    //  Movement
    public float speed = 5;

    //  Cursor
    public float cursorRange;

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update() {
        move();
        orient();
        
    }

    public void move() {
        float xDisp = Input.GetAxis("LeftJoystickHorizontal");
        float zDisp = -1 * Input.GetAxis("LeftJoystickVertical");

        Vector3 forward = pivotPoint.transform.TransformDirection(Vector3.forward);
        forward.y = 0;
        forward = forward.normalized;

        Vector3 right = new Vector3(forward.z, 0, -forward.x);
        
        Vector3 dispDir = (xDisp * right + zDisp * forward);
        if (dispDir.sqrMagnitude > 0.4) {
            dispDir *= speed;
            cc.SimpleMove(dispDir);
            orient(Mathf.Atan2(-1 * dispDir.z, dispDir.x) * Mathf.Rad2Deg);
        }
    }

    public void orient() {
        float camOrientation = pivotPoint.transform.rotation.eulerAngles.y;


        Vector3 orientation = Vector3.zero;
        orientation.x = Input.GetAxis("RightJoystickVertical");
        orientation.z = Input.GetAxis("RightJoystickHorizontal");

        switch (CameraControls.type) {
            case 0:
                if (Input.GetAxis("Trigger") >= 0.9) {
                    orientation.x = Input.GetAxis("RightJoystickVertical");
                    orientation.z = Input.GetAxis("RightJoystickHorizontal");

                    if (orientation.sqrMagnitude >= 0.01) {
                        //transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg + camOrientation, transform.eulerAngles.z);
                        orient(Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg + camOrientation);
                        cursor.transform.localPosition = new Vector3(cursorRange * orientation.magnitude, 0, 0);
                        //Handles.DrawBezier
                    } else {
                        cursor.transform.localPosition = new Vector3(0, 0, 0);
                    }
                }
                break;
            case 1:
                orientation.x = Input.GetAxis("RightJoystickVertical");
                orientation.z = Input.GetAxis("RightJoystickHorizontal");

                if (orientation.sqrMagnitude >= 0.01) {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg + camOrientation, transform.eulerAngles.z);
                    cursor.transform.localPosition = new Vector3(cursorRange * orientation.magnitude, 0, 0);
                    //Handles.DrawBezier
                } else {
                    cursor.transform.localPosition = new Vector3(0, 0, 0);
                }
                break;
        }
    }

    public void orient(float angle) {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
    }

    public void jump() {

    }

    public void setPivotPoint(GameObject pivotPoint) {
        this.pivotPoint = pivotPoint;
    }
}