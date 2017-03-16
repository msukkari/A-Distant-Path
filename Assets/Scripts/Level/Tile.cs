using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {
    public EventTransferManager EventManager = null; // Instantiated in Level Manager

	public int id;

	public List<Tile> neighbors = new List<Tile>();

	private Level level;
	//private List<int> nIDList;

	public Element element;
	public bool navigatable = true;
	public bool isGroundTile = true;

	public Vector3 tileScale = new Vector3 (1.0f, 1.0f, 1.0f);

	// MATERIALS //
	public Material GrassMat;

    public bool isHighlighted;

    private Renderer renderer;

	void Start(){
		element = GetComponentInChildren<Element> ();

		SetNavigatable((this.element == null) ? true : this.element.navigatable);

		this.setMaterial();

		this.renderer = this.GetComponent<Renderer>();
	}

	
	// Update is called once per frame
	void Update (){
		if(isHighlighted){
			renderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");

			if(this.element != null && this.element.highlightable){
				if(renderer != null)
					renderer.material.shader = Shader.Find("Self-Illumin/Outlined Diffuse");
			}
		}
		else{
			renderer.material.shader = Shader.Find("Diffuse");

			if(this.element != null && this.element.highlightable){
				if(renderer != null)
					renderer.material.shader = Shader.Find("Diffuse");
			}
		}
	}

	// PUBLIC METHODS //
	public int getTileID(){return id;} // getter for id
	//public List<int> getNTileIDList(){return this.nIDList;} // getter for neighbor tile ID list

	// calculates and sets the tile ID
	public void calcID(){
		this.id = (int)(transform.position.x  * GameManager.GRID_SIZE) + (int)transform.position.z;
	}


	// initialization of the tile goes here (tile ID calculation must be done before this method since some methods in init require all tile ID's to be calculated)
	public void initTile(){
		// Get the neighbor list and then validate it
		//this.fetchNeighborTiles();
		//this.validateNList();

		// Set the material of the tile based on what state it is
		//this.setMaterial();

		this.name = "Tile " + id;
		this.gameObject.tag = "Tile";
	}

	#region Tile Element Methods

	public bool HasElement() {
		return this.element != null;
	}

	public bool GainElement(ElementType elementType) {
		ElementType newElement = (this.element == null) ? elementType : ElementManager.GetCombinationElement(elementType, this.element.elementType);

		Debug.Log(newElement);

		if(newElement == ElementType.None){
			ClearElement();
			return true;
		}
		else if (this.element == null || newElement != this.element.elementType) {
			ClearElement ();
			LevelManager.CreateElementAtTile (this, newElement);

			if (this.element.elementType == ElementType.Water) {
				this.element.GetComponent<Water> ().destroyTileOnLose = false;
			}

			setMaterial ();
			return true;
		}

		return false;
	}

	public ElementType LoseElement() {
		ElementType elementLost = this.element.elementType;
		element.quantity--;

		if (element.quantity <= 0) {
			ClearElement ();
		}

		return elementLost;
	}

	public void ClearElement() {
		if (this.element != null) {
			ElementType type = this.element.elementType;
			if (type == ElementType.Water && this.element.GetComponent<Water> ().destroyTileOnLose) {
				this.GetComponent<MeshRenderer> ().enabled = false;
				this.enabled = false;
			} else {
				this.SetNavigatable (true);
			}

			Destroy (element.gameObject);
			this.element = null;

			setMaterial ();
		}
	}

	#endregion


	public void SetNavigatable(bool navigatable) {
		this.navigatable = navigatable;
		BoxCollider collider = this.GetComponent<BoxCollider> ();

		if(!navigatable && this.element != null && this.element.elementType != ElementType.Transfer)
			collider.size = new Vector3(collider.size.x, 2.5f, collider.size.z);
		else if(navigatable){
			collider.size = new Vector3(collider.size.x, 1.0f, collider.size.z);
		}
		
	}

	public bool HasElementOfType(ElementType elementType) {
		return this.element != null && this.element.elementType == elementType;
	}

	public double getDistance(Tile goal) {
		return Vector3.Distance(this.transform.position, goal.transform.position);
	}


	// PRIVATE METHODS //


	// Sets the material of the tile based on the element it has
	private void setMaterial(){
		Renderer renderer = GetComponent<Renderer>();
		renderer.material = GrassMat;

		if (element != null) {
			if (element.elementType == ElementType.Ice || element.elementType == ElementType.Sand
			   || element.elementType == ElementType.MoltenSand || element.elementType == ElementType.Glass) {
				renderer.material = element.GetComponent<Renderer>().material;
			}
		}
		/*
		if (element != null) {
			switch (element.elementType) {
			case ElementType.Glass:
				break;
			case ElementType.Ice:
				renderer.material = IceMat;
				//this.element.GetComponent<MeshRenderer> ().enabled = false;
				//this.element.GetComponent<Collider> ().enabled = false;
				SetNavigatable (true);
				break;
			case ElementType.MoltenSand:
			case ElementType.Sand:
				break;
			default:
				renderer.material = GrassMat;
				break;
			}
		}
		else{
			renderer.material = GrassMat;
		}*/
	}


    // Removes invalid tile ID's (ID's that don't represent an actual tile)
    /*
	private void validateNList(){
		List<int> validList = new List<int>();

		for(int i = 0; i < nIDList.Count; i++){
			Tile tile = LevelManager.instance.getTileAt(nIDList[i]);
			if(tile != null){
				validList.Add(tile.getTileID());
			}
		}

		this.nIDList = validList;
	}
	*/


    /* OLD NEIGHBOR FETCH METHOD
	// Gets all the neighboring tile ID's and puts them in nIDList (NOTE: list must be validated! See valideNList())
	private void fetchNeighborTiles(){
		List<int> result = new List<int>();
		int maxTileID = (GameManager.GRID_SIZE * GameManager.GRID_SIZE) - 1;

		// Forward is considered to be in the positive z-axis
		// Right is considered to be in the positive x-axis

		int forward = this.id + 1;
		if(forward >= 0 && forward <= maxTileID) result.Add(forward);

		int back = this.id - 1;
		if(back >= 0 && back <= maxTileID) result.Add(back);

		int right = this.id + GameManager.GRID_SIZE;
		if(right >= 0 && right <= maxTileID) result.Add(right);

		int left = this.id - GameManager.GRID_SIZE;
		if(left >= 0 && left <= maxTileID) result.Add(left);

		int forward_right = this.id + 1 + GameManager.GRID_SIZE;
		if(forward_right >= 0 && forward_right <= maxTileID) result.Add(forward_right);

		int forward_left = this.id + 1 - GameManager.GRID_SIZE;
		if(forward_left >= 0 && forward_left <= maxTileID) result.Add(forward_left);

		int back_right = this.id - 1 + GameManager.GRID_SIZE;
		if(back_right >= 0 && back_right <= maxTileID) result.Add(back_right);

		int back_left = this.id - 1 - GameManager.GRID_SIZE;
		if(back_left >= 0 && back_left <= maxTileID) result.Add(back_left);

		this.nIDList = result;
	}
	*/
    private Vector3 offset = new Vector3(0f, 1.5f, 0f);
    private GameObject highlightObj;
    public void highlight() {
        if (!isHighlighted) {
            highlightObj = Instantiate(Resources.Load("Other/Highlight"), transform.position +offset, new Quaternion(), transform) as GameObject;

            //GameObject highlight = Instantiate(Resources.Load("Other/Highlight")) as GameObject;
            //highlight.transform.parent = transform;
            isHighlighted = true;
        }
    }

    public void unHighlight() {
        Destroy(highlightObj);
        isHighlighted = false;
    }

	private Tile GetTileAbove() {
		RaycastHit hit;

		Debug.DrawLine (this.transform.position, this.transform.position + 3.0f * Vector3.up);
		if (Physics.Raycast (this.transform.position, Vector3.up, out hit)) {
			if (hit.collider.tag == "Tile") {
				return hit.collider.gameObject.GetComponent<Tile>();
			}
		}
		return null;
	}

	public Tile GetTopTile() {
		Tile topTile = this;
		while (topTile.GetTileAbove () != null) {
			topTile = topTile.GetTileAbove ().GetComponent<Tile> ();
		}
		return topTile;
	}

}