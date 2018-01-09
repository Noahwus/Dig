using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateTraderFromBigBook : MonoBehaviour {

	//trader's stuff
	public List<InvItem> traderInventory = new List<InvItem> ();
	//big book of items
	private ItemMaster masterList;
	
	//time to wait before updating list again
	float updateTraderInventoryTime = 0.01f;

	//item to instantiate and copy
	public GameObject itemInInventory;
	//parent for instances
	public GameObject inventoryContentArea;

	//making masterList mean something
	void Awake(){
		masterList = FindObjectOfType<ItemMaster> ();
	}

	//restarts coroutine when object is active
	void OnEnable(){
		StartCoroutine (updateTraderInventory ());
	}

	IEnumerator updateTraderInventory(){
		while (enabled){
			updateInventory ();
			inventoryAddInstances ();
			inventoryRemoveInstances ();
			yield return new WaitForSeconds (updateTraderInventoryTime);
		}

	}

	void updateInventory (){
		for (int i = 0; i < masterList.itemMasterList.Count; i ++) {
			InvItem myitem = masterList.itemMasterList[i];
			if (myitem.traderOwns) {
				if (traderInventory.Contains (myitem)) {
					//Debug.Log ("already in inventory yo");
				} else {
					traderInventory.Add (myitem);
					//Debug.Log ("we added "+ myitem.title +" because it was supposed to be here");
				}

			} else {
				if (traderInventory.Contains (myitem)) {
					traderInventory.Remove (myitem);
					//Debug.Log ("we did removed " + myitem.title + " because it wasn't supposed to be there");
				} else {
					//Debug.Log ("we did nothing because " + myitem.title + " wasn't supposed to be there");
				}
			}

			Debug.Log (traderInventory.Count);
		}

	}

	void inventoryAddInstances(){

		//inventory.ForEach (Instantiate (itemInInventory, inventoryContentArea.transform));
		// for each thing in the inventory, check if there is an instance of it already, if not, create one. 
		//if there is an instance and there should not be an instance, destroy it.
		for (int w = 0; w < traderInventory.Count; w++) {

			InvItem myitem = traderInventory[w];

			if (GameObject.Find(myitem.name + "trader") != null){

				//Debug.Log (myitem.title + " does have an instance (add)");


			}
			if (GameObject.Find(myitem.name + "trader") == null){
				//create the instance
				var clone = Instantiate (itemInInventory, inventoryContentArea.transform);
				//set the name to the item name so it is findable
				clone.name = myitem.name + "trader";
				//check that it's got the right item
				//Debug.Log (myitem.itemNumber + " item number");
				//grab the script that creates the visuals and the text
				UseItemToPopulateFields popfield = clone.GetComponent<UseItemToPopulateFields> ();
				//set the field of the item so it populates correct images and text
				popfield.itemNumber = myitem.itemNumber;
				//double check that the numbers set correctly
				//Debug.Log (popfield.itemNumber + " pop field number");
			}

		}


	}

	void inventoryRemoveInstances(){

		//inventory.ForEach (Instantiate (itemInInventory, inventoryContentArea.transform));
		// for each thing in the inventory, check if there is an instance of it already, if not, create one. 
		//if there is an instance and there should not be an instance, destroy it.
		for (int x = 0; x < masterList.itemMasterList.Count ; x++) {

			InvItem myitem = masterList.itemMasterList[x];

			if (GameObject.Find(myitem.name + "trader") != null){

				//Debug.Log (myitem.title + " does have an instance, will be checked");

				//check that it should be there

				if (myitem.traderOwns) {
					//Debug.Log (myitem.title + " should be here, will not be destroyed");
					if (myitem.quantity <= 0){
						Destroy (GameObject.Find(myitem.name));
						Debug.Log (myitem.title + " instance was destroyed");	
					}
				} 
				if (myitem.traderOwns != true){
					Destroy (GameObject.Find(myitem.name + "trader"));
					Debug.Log (myitem.title + " instance was destroyed");	
				}



			}
			if (GameObject.Find(myitem.name + "trader") == null){
				//Debug.Log ("nothing to destroy");

			}

		}


	}

}
