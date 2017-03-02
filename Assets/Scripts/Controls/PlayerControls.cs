using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    //External Objects
    public CharacterController cc;
    public GameObject pivotPoint;
    public GameObject cursor;

    public Animator anim;

    //Parameters
    //  Movement
    private float speed = 3.5f;

    //  Cursor
    private float actualCursorRange;
    private float cursorRange = 5f;

    private float joystickThreshold = 0.3f;

    private GameObject selectedTile;
    private GameObject prevSelectedTile;

    private TriggerType mode;

    private float jumpForce = 10;
    private float verticalVelocity;

    private int numTypes = 3;
    public enum TriggerType {
        placeWaypoint,
        arcThrowing,
        directInteract,
    }

    private Player playerScript;

    // Use this for initialization
    void Start() {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        mode = TriggerType.directInteract;
        playerScript = gameObject.GetComponent<Player>();
    }



    // Update is called once per frame
    void Update() {
        move();
        orient();

        if(cc.isGrounded){
        	verticalVelocity = -9.81f * Time.deltaTime;
        	anim.SetBool("Jumping", false);

        	if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton")){
        		verticalVelocity = jumpForce;
        		anim.SetBool("Jumping", true);
        	}
        }


        cc.Move(new Vector3(0, verticalVelocity, 0) * Time.deltaTime);


        if (Input.GetButtonDown("LeftBumper")) {
            toggleMode();
        }

        if(mode == TriggerType.arcThrowing || mode == TriggerType.placeWaypoint) {
            actualCursorRange = cursorRange;
            cursor.GetComponent<MeshRenderer>().enabled = true;
        } else if(mode == TriggerType.directInteract) {
            actualCursorRange = 0.4f;
            cursor.GetComponent<MeshRenderer>().enabled = false;

        }

        if (Input.GetAxis("RightTrigger") >= 0.9 || mode == TriggerType.directInteract) {

            RaycastHit hit = new RaycastHit();
            Ray ray = new Ray(cursor.transform.position + new Vector3(0, 50, 0), Vector3.down);

            if (Physics.Raycast(ray, out hit)) {
                prevSelectedTile = selectedTile;
                selectedTile = hit.collider.gameObject;
                if (selectedTile != null) {
                    if (prevSelectedTile != selectedTile) {
                        selectedTile.GetComponent<Tile>().highlight();

                        if (prevSelectedTile != null) {
                            prevSelectedTile.GetComponent<Tile>().unHighlight();
                        }
                    }
                }
            }
        } else {
            if (selectedTile != null) {
                selectedTile.GetComponent<Tile>().unHighlight();
            }
        }


        int curID = getIDUnderCursor();

        if (Input.GetAxis("LeftTrigger") >= 0.9) {
            if (mode == TriggerType.arcThrowing) {
                playerScript.throwMaterial(curID);
            } else if (mode == TriggerType.directInteract) {
                playerScript.interactInFront(curID);
            } else if (mode == TriggerType.placeWaypoint) {
                playerScript.placeWaypoint(cursor.transform.position);
            }
        }
    }

    public void move() {
        float xDisp = Input.GetAxis("LeftJoystickHorizontal");
        float zDisp = -1 * Input.GetAxis("LeftJoystickVertical");

        if((Mathf.Abs(xDisp) >= 0.1) || (Mathf.Abs(zDisp) >= 0.1)){
        	anim.SetBool("Moving", true);
        }
        else{
        	anim.SetBool("Moving", false);
        }

        Vector3 dispDir = Vector3.zero;

        if (Mathf.Abs(xDisp) >= joystickThreshold || Mathf.Abs(zDisp) >= joystickThreshold) {
            Vector3 forward = pivotPoint.transform.TransformDirection(Vector3.forward);
            forward.y = 0;
            forward = forward.normalized;

            Vector3 right = new Vector3(forward.z, 0, -forward.x);
            dispDir = (xDisp * right + zDisp * forward);
            dispDir *= speed;
            orient(Mathf.Atan2(-1 * dispDir.z, dispDir.x) * Mathf.Rad2Deg);
        }
        cc.SimpleMove(dispDir);
    }

    public void orient() {
        float camOrientation = pivotPoint.transform.rotation.eulerAngles.y;


        Vector3 orientation = Vector3.zero;
        orientation.x = Input.GetAxis("RightJoystickVertical");
        orientation.z = Input.GetAxis("RightJoystickHorizontal");

        switch (CameraControls.type) {
            case 0:
                if (Input.GetAxis("RightTrigger") >= 0.9) {
                    orientation.x = Input.GetAxis("RightJoystickVertical");
                    orientation.z = Input.GetAxis("RightJoystickHorizontal");

                    if (orientation.sqrMagnitude >= 0.01) {
                        orient(Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg + camOrientation);
                        cursor.transform.localPosition = new Vector3(0, 0, Mathf.Clamp(actualCursorRange * orientation.magnitude,2f, actualCursorRange));
                    } else {
                        cursor.transform.localPosition = new Vector3(0, 0, 0.5f);
                    }
                } else {
                    cursor.transform.localPosition = new Vector3(0, 0, 0.5f);
                }
                break;
            case 1:
                orientation.x = Input.GetAxis("RightJoystickVertical");
                orientation.z = Input.GetAxis("RightJoystickHorizontal");

                if (orientation.sqrMagnitude >= 0.01) {
                    transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(orientation.x, orientation.z) * Mathf.Rad2Deg + camOrientation + 90, transform.eulerAngles.z);
                    cursor.transform.localPosition = new Vector3(0, 0, actualCursorRange * orientation.magnitude);
                    //Handles.DrawBezier
                } else {
                    cursor.transform.localPosition = new Vector3(0, 0, 0);
                }
                break;
        }
    }

    public void orient(float angle) {
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, angle + 90, transform.eulerAngles.z);
    }

    public void jump() {

    }

    public void setPivotPoint(GameObject pivotPoint) {
        this.pivotPoint = pivotPoint;
    }

    public void toggleMode() {
        int i = (int)mode;
        i++;
        i %= numTypes;
        mode = (TriggerType)i;

        /*if (mode == TriggerType.placeWaypoint || mode == TriggerType.arcThrowing) {
            cursor.GetComponent<MeshRenderer>().enabled = true;
        } else if (mode == TriggerType.directInteract) {
            cursor.GetComponent<MeshRenderer>().enabled = false;
        }*/
    }

    public int getIDUnderCursor(){
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(cursor.transform.position + new Vector3(0, 50, 0), Vector3.down);

        if (Physics.Raycast(ray, out hit)) {
            GameObject tileGO = hit.collider.gameObject;
            if (tileGO != null) {
                Tile tile = tileGO.GetComponent<Tile>();

                if (tile != null) {
                    return tile.getTileID();
                }     
            }
        }

        Debug.Log("ERROR GETTING ID UNDER CURSOR");
        return -1;
    }
}