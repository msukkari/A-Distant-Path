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

    private float jumpForce = 3f;
    public float verticalVelocity;

    private bool isShooting = false;


    // 0 is fire, 1 is water
    private int currentAmmo = 0;

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

        if(Input.GetButtonDown("YButton")){
        	climb();
        }

        if(Input.GetButtonDown("BButton")){
        	currentAmmo = (currentAmmo + 1) % 2;
        }

        if(cc.isGrounded){
        	verticalVelocity = -9.81f * Time.deltaTime;
        	anim.SetBool("isJumping", false);

        	if(Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("AButton")){
        		verticalVelocity = jumpForce;
        		anim.SetBool("isJumping", true);
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
            actualCursorRange = 1.8f;
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
                        Tile curtile = selectedTile.GetComponent<Tile>();

                        if(curtile != null){
                            selectedTile.GetComponent<Tile>().unHighlight();
                        }

                        if (prevSelectedTile != null) {
                            Tile prevtile = prevSelectedTile.GetComponent<Tile>();

                            if(prevtile != null){
                                prevSelectedTile.GetComponent<Tile>().unHighlight();
                            }
                        }
                    }
                }
            }
        } else {
            if (selectedTile != null) {
                Tile tile = selectedTile.GetComponent<Tile>();

                if(tile != null){
                    selectedTile.GetComponent<Tile>().unHighlight();
                }
            }
        }


        Tile curTile = getTileUnderCursor();


        // Debug.Log(isShooting);
        if (Input.GetAxis("LeftTrigger") >= 0.9 && !isShooting) {
        	isShooting = true;
            if (mode == TriggerType.arcThrowing) {
                playerScript.throwMaterial(curTile);
            } else if (mode == TriggerType.directInteract) {
                playerScript.interactInFront(curTile, currentAmmo);
            } else if (mode == TriggerType.placeWaypoint) {
                playerScript.placeWaypoint(cursor.transform.position);
            }
        }
        else{
        	// Debug.Log("SETTING SHOOTING TO FALSE");
        	isShooting = false;
        }
    }

    public void move() {
        float xDisp = Input.GetAxis("LeftJoystickHorizontal");
        float zDisp = -1 * Input.GetAxis("LeftJoystickVertical");

        if((Mathf.Abs(xDisp) >= 0.1) || (Mathf.Abs(zDisp) >= 0.1)){
        	anim.SetBool("isMoving", true);
        }
        else{
        	anim.SetBool("isMoving", false);
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
                        cursor.transform.localPosition = new Vector3(0, 0, 1.8f);
                    }
                } else {
                    cursor.transform.localPosition = new Vector3(0, 0, 1.8f);
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

    public Tile getTileUnderCursor(){
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(cursor.transform.position + new Vector3(0, 50, 0), Vector3.down);

        if (Physics.Raycast(ray, out hit)) {
            GameObject tileGO = hit.collider.gameObject;
            if (tileGO != null) {
                Tile tile = tileGO.GetComponent<Tile>();

                if (tile != null) {
                    return tile;
                }     
            }
        }

        Debug.Log("ERROR GETTING TILE UNDER CURSOR");
        return null;
    }



    public void climb(){
    	Tile frontTile = null;



    	Tile tile = getTileUnderCursor();

    	if(tile != null){
    		frontTile = tile;
    	}

    	/*
    	RaycastHit hit = new RaycastHit();
    	Debug.DrawRay(this.gameObject.transform.position, this.transform.forward, Color.red, 5);
        Ray ray = new Ray(this.gameObject.transform.position + new Vector3(0, 0.2f, 0), this.transform.forward);


        if (Physics.Raycast(ray, out hit)) {
            GameObject tileGO = hit.collider.gameObject;

            if (tileGO != null && hit.distance < 1) {
                	Debug.Log("GLOBAL: " + tileGO.transform.position + " LOCAL: " + tileGO.transform.localPosition);
                    frontTile = tileGO;
            }
        }
        */


        if(frontTile != null){

        	float heightDiff = frontTile.transform.position.y - playerScript.gameObject.transform.position.y;
        	Debug.Log(heightDiff);
        	if(heightDiff < 1.6){
        		StartCoroutine(climbWithStall(frontTile));
        	}
        	else{
        		Debug.Log("THE TILE THE PLAYER IS TRYING TO CLIMB IS TOO HIGH");
        	}

        }
        else{
        	Debug.Log("Climbing failed, either not close enough to front tile or no such tile exists");
        }

    }

    IEnumerator climbWithStall(Tile tile){
    	//PlayerMesh mesh = this.GetComponentInChildren<PlayerMesh>();
    	
    	//mesh.enableMesh(false);
    	yield return new WaitForSeconds(0.2f);

    	Vector3 newPosition;
    	if(tile.element != null && tile.element.climable){
    		newPosition = tile.element.gameObject.transform.position;
    	}
    	else{
    		newPosition = tile.transform.position;
    	}


    	this.transform.position = new Vector3(newPosition.x, newPosition.y + 1f, newPosition.z); // height is hard coded for now

    	//mesh.enableMesh(true);
    }
}