using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class PopulateInventoryListFromItemsArray : MonoBehaviour {

	//current items go into this list
	List<InvItem> inventory = new List<InvItem> ();
	//big book of all items here
	private ItemMaster masterList;

	//time to wait before updating the inventory list again
	float updateInventoryTime = 0.5f;

	// Use this for initialization
	void Start () {
		StartCoroutine (updateInventoryList ());
	}
	//making masterList mean something
	void Awake(){
		masterList = FindObjectOfType<ItemMaster> ();
	}
	// Update is called once per frame
	void Update () {
		
		Debug.Log ("update"+ masterList.itemMasterList.Count); 

	}

	void addToListFromArray (){
		for (int i = 0; i < masterList.itemMasterList.Count; i ++) {
			InvItem myitem = masterList.itemMasterList[i];
			if (myitem.isInInventory) {
				Debug.Log ("already in inventory yo");
			} else {
				inventory.Add (myitem);
				myitem.isInInventory = true;
				Debug.Log ("yo we added something whoopeeee");
			}

			//Debug.Log (inventory.Count);
		}


		//foreach (InvItem item  in master) {
			
			//if (item.isInInventory) {
				//var myitem = new InvItem ();
				//inventory.Add (item);
			//}
		//}

	}

	IEnumerator updateInventoryList(){
		while (enabled){
			addToListFromArray ();
			yield return new WaitForSeconds (updateInventoryTime);
		}

	}



}
