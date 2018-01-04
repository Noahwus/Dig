using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour {

	public float topSpeed = 10f;

	bool facingRight = true;

	//get reference to animator
	Animator anim;

	//not grounded
	bool grounded = false;

	//empty game object at feet for transform
	public Transform groundCheck;

	//groundcheck radius of checking
	float groundRadius = 0.2f;

	//force of the jump
	public float jumpForce = 700f;

	//what layer is considered the ground
	public LayerMask whatIsGround;

	GameObject tempGameObject;

	//big book of all items here
	private ItemMaster masterList;

	private DroppableDefinitions tempObjectDef;

	public bool isLeftClicking;

	public GameObject baseballCap;
	public GameObject Dirt;

	//counting the inventory on pickup
	int InventoryCount;

	void Start(){
		anim = GetComponent<Animator> ();
		masterList = FindObjectOfType<ItemMaster> ();
	}

	void FixedUpdate(){
		//true or false did the ground transform hit anything in the layer ground with the circle radius we set
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround);

		//tell the animator that we are on the ground
		anim.SetBool ("Ground", grounded);

		//how fast we are moving upwards or downwards
		anim.SetFloat ("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

		//get move direction
		float move = Input.GetAxis("Horizontal");

		//add velocity to the rigidbody in the move direction * our speed
		GetComponent<Rigidbody2D>().velocity = new Vector2(move* topSpeed, GetComponent<Rigidbody2D>().velocity.y);

		anim.SetFloat ("Speed", Mathf.Abs(move));

		//set the holding shift bool
		if (grounded && Input.GetKey (KeyCode.LeftShift)) {
			anim.SetBool ("holdingShift", true);
			topSpeed = 8;
		} else {
			anim.SetBool ("holdingShift", false);
			topSpeed = 5;
		}

		//spawning test items
		if (Input.GetMouseButton(1)){
			Debug.Log ("spawning new item");
			Instantiate (baseballCap, Camera.main.ScreenToWorldPoint (Input.mousePosition + new Vector3 (0, 0,120)), Quaternion.identity);
			Instantiate (Dirt, Camera.main.ScreenToWorldPoint (Input.mousePosition + new Vector3 (0, 0,120)), Quaternion.identity);
		}


		//checking if they're clicking 
		if (Input.GetMouseButton (0)) {
			//isLeftClicking = true;
			anim.SetBool ("leftClick", true);
			Debug.Log ("and we're clicking...");

			//casting a ray to find the gameObject they are clicking on
			RaycastHit2D hit = Physics2D.Raycast (Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);	

			if (hit.collider != null){
				Debug.Log (hit.collider.transform.gameObject.name);

				if (hit.collider.transform.gameObject.tag == "ground"){
					tempGameObject = hit.collider.transform.gameObject;
					Instantiate (Dirt, tempGameObject.transform.position, Quaternion.identity);
					Destroy (tempGameObject, 1.5f);			
				}
				}
			} else {
				//isLeftClicking = false;
				anim.SetBool ("leftClick", false);
		}


		//if we're facing the negative direction and not facing the right, flip
		if (move > 0 && !facingRight) {
			Flip ();

		} else if (move < 0 && facingRight) {
			Flip ();	
		}
			
			
	}

	//colliding with droppables!
	void OnCollisionEnter2D(Collision2D coll){
		if(coll.gameObject.tag == "dropped"){
			tempGameObject = coll.gameObject;
			Debug.Log ("collided with "+ tempGameObject.name);
			inventoryCheckIfFull ();

			//if it is full, don't add it to the inventory
			if (inventoryCheckIfFull()){
				Debug.Log ("Inventory Full");
				//check if there is already one of that type there, and if so add to the quantity instead
				tempObjectDef = tempGameObject.GetComponent<DroppableDefinitions> ();
				var itemNumber = tempObjectDef.itemNumber;
				if(masterList.itemMasterList[itemNumber].isInInventory == true){
					masterList.itemMasterList[itemNumber].quantity = (masterList.itemMasterList[itemNumber].quantity + 1);
					Destroy (tempGameObject);
				}
			}

			//if it is not full, add it to the inventory
			if(!inventoryCheckIfFull()){
				Debug.Log ("Inventory not full, adding " + tempGameObject.name.ToString());
				tempObjectDef = tempGameObject.GetComponent<DroppableDefinitions> ();
				var itemNumber = tempObjectDef.itemNumber;
				if(masterList.itemMasterList[itemNumber].isInInventory == true){
					Debug.Log ("added to quantity");
					masterList.itemMasterList[itemNumber].quantity = (masterList.itemMasterList[itemNumber].quantity + 1);
					Destroy (tempGameObject);
				}
				if(masterList.itemMasterList[itemNumber].isInInventory == false){
					Debug.Log ("added to inventory");
					masterList.itemMasterList[itemNumber].isInInventory = true;
					Destroy (tempGameObject);
				}

			}
		}
	
	}

	void Update(){
		if (grounded && Input.GetKeyDown(KeyCode.W)){
			//not on the ground
			anim.SetBool ("Ground", false);

			//add jump force on the y in an impulse fashion
			GetComponent<Rigidbody2D> ().AddForce (new Vector2(0, jumpForce), ForceMode2D.Impulse);

		
		}
	}

	void Flip(){
		//saying we are facing the opposite direction
		facingRight = !facingRight;

		//get the local scale
		Vector3 theScale = transform.localScale;

		//flip on the x axis
		theScale.x *= -1;

		//apply that to the localscale
		transform.localScale = theScale;


	}

	public bool inventoryCheckIfFull(){
		InventoryCount = 0;
		//go through big book, check how many things are marked in the inventory, and then decide whether a item to be picked up should be
		for (int x = 0; x < masterList.itemMasterList.Count ; x++) {

			InvItem myitem = masterList.itemMasterList[x];

			if (myitem.isInInventory){
				InventoryCount = InventoryCount + 1;
			}



		}

		if (InventoryCount >= 9) {
			//inventory is full can't hold any more, don't pick it up
			Debug.Log ("Inventory Full");
			return true;

		} else {
			return false;
		}
	}


}
