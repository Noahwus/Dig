using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PopulateInventoryListFromItemsMaster : MonoBehaviour {

	//current items go into this list
	List<InvItem> inventory = new List<InvItem> ();
	//big book of all items here
	private ItemMaster masterList;

	//totalamount inventorysize
	public int inventorySize = 18;

	//time to wait before updating the inventory list again
	float updateInventoryTime = 0.01f;

	//item to instantiate and copy 
	public GameObject itemInInventory;
	//parent for instances
	public GameObject inventoryContentArea;

	// Use this for initialization
	void Start () {
		
	}
	//making masterList mean something
	void Awake(){
		masterList = FindObjectOfType<ItemMaster> ();

	}
	//restarts coroutine when object is active
	void OnEnable(){
		StartCoroutine (updateInventoryList ());
	}
	// Update is called once per frame
	void Update () {
		
		Debug.Log ("update"+ masterList.itemMasterList.Count); 

	}

	IEnumerator updateInventoryList(){
		while (enabled){
			updateInventory ();
			inventoryInstances ();
			yield return new WaitForSeconds (updateInventoryTime);
		}

	}


	void updateInventory (){
		for (int i = 0; i < masterList.itemMasterList.Count; i ++) {
			InvItem myitem = masterList.itemMasterList[i];
			if (myitem.isInInventory) {
				if (inventory.Contains (myitem)) {
					Debug.Log ("already in inventory yo");
				} else {
					inventory.Add (myitem);
					Debug.Log ("we added "+ myitem.title +" because it was supposed to be here");
				}

			} else {
				if (inventory.Contains (myitem)) {
					inventory.Remove (myitem);
					Debug.Log ("we did removed " + myitem.title + " because it wasn't supposed to be there");
				} else {
					Debug.Log ("we did nothing because " + myitem.title + " wasn't supposed to be there");
				}
			}

			Debug.Log (inventory.Count);
		}


		//foreach (InvItem item  in master) {
			
			//if (item.isInInventory) {
				//var myitem = new InvItem ();
				//inventory.Add (item);
			//}
		//}

	}

	void inventoryInstances(){
		
		//inventory.ForEach (Instantiate (itemInInventory, inventoryContentArea.transform));
		// for each thing in the inventory, check if there is an instance of it already, if not, create one. 
		//if there is an instance and there should not be an instance, destroy it.
		for (int w = 0; w < inventory.Count; w ++) {
			

			InvItem myitem = inventory[w];
			//Instantiate()
			if (GameObject.Find(myitem.name) != null){
				Debug.Log (myitem.title + " does have an instance");
			}
			if (GameObject.Find(myitem.name) == null){
				var clone = Instantiate (itemInInventory, inventoryContentArea.transform);
				clone.name = myitem.name;
				myitem.itemNumber = w;
				Debug.Log (myitem.itemNumber + " item number");
				UseItemToPopulateFields popfield = clone.GetComponent<UseItemToPopulateFields> ();
				popfield.itemNumber = myitem.itemNumber;
				Debug.Log (popfield.itemNumber + " pop field number");


			}





		
	}


}
}
