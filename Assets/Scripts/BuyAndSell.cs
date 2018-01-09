using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BuyAndSell : MonoBehaviour, IPointerClickHandler {

	//	private PopulateInventoryListFromItemsMaster currentInventory;
	public int itemNumber;
	private UseItemToPopulateFields currentItem;
	private ItemMaster masterList;
	private UseItemToPopulateFields EquippedItemPopField;

	GameObject alreadyEquippedItem;

	public UnityEvent leftClick;
	public UnityEvent middleClick;
	public UnityEvent rightClick;


	void Awake(){
		//currentInventory = FindObjectOfType<PopulateInventoryListFromItemsMaster> ();
		currentItem = FindObjectOfType<UseItemToPopulateFields> ();
		masterList = FindObjectOfType<ItemMaster> ();

	}
	// Update is called once per frame

	public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Left)
			leftClick.Invoke ();
		else if (eventData.button == PointerEventData.InputButton.Middle)
			middleClick.Invoke ();
		else if (eventData.button == PointerEventData.InputButton.Right)
			rightClick.Invoke ();
	}



	public void Sell(){
		itemNumber = currentItem.itemNumber;
		//Debug.Log (masterList.itemMasterList [itemNumber].itemNumber + " current item number");
		Debug.Log("Attempting to Sell: " + masterList.itemMasterList [itemNumber].title);

		//check if it's in the player's inventory
		if (masterList.itemMasterList [itemNumber].isInInventory) {
			Debug.Log ("it is in the inventory");
			//check if it has more than one of it
			if (masterList.itemMasterList [itemNumber].quantity > 1) {
				Debug.Log ("there is more than one");
				//deduct one from the quantity, and add gems to player total gems
				masterList.itemMasterList [itemNumber].quantity = masterList.itemMasterList [itemNumber].quantity - 1;
				masterList.itemMasterList [itemNumber].traderQuantity = masterList.itemMasterList [itemNumber].traderQuantity + 1;
				masterList.itemMasterList [itemNumber].traderOwns = true;
			} else {
				Debug.Log ("there is only one");
				//there was only one, take it out of the inventory, and add gems
				masterList.itemMasterList [itemNumber].isInInventory = false;
				masterList.itemMasterList [itemNumber].quantity = masterList.itemMasterList [itemNumber].quantity - 1;
				masterList.itemMasterList [itemNumber].traderQuantity = masterList.itemMasterList [itemNumber].traderQuantity + 1;
				masterList.itemMasterList [itemNumber].traderOwns = true;
			}

		} else {
			//was not in their inventory,cannot sell something you don't have

		}

	}

	public void Buy(){
		itemNumber = currentItem.itemNumber;
		//Debug.Log (masterList.itemMasterList [itemNumber].itemNumber + " current item number");
		Debug.Log("Attempting to Buy: " + masterList.itemMasterList [itemNumber].title);

		//check if it's in the player inventory
		if (masterList.itemMasterList [itemNumber].isInInventory) {
			//add to the quantity and deduct gems from total
			masterList.itemMasterList [itemNumber].quantity = masterList.itemMasterList [itemNumber].quantity + 1;
			masterList.itemMasterList [itemNumber].traderQuantity = masterList.itemMasterList [itemNumber].traderQuantity - 1;
			if (masterList.itemMasterList [itemNumber].traderQuantity < 1){
				masterList.itemMasterList [itemNumber].traderOwns = false;
			}
		} else {
			//wasn't in their inventory, add it to their inventory and deduct gems
			masterList.itemMasterList [itemNumber].isInInventory = true;
			masterList.itemMasterList [itemNumber].quantity = masterList.itemMasterList [itemNumber].quantity + 1;
			masterList.itemMasterList [itemNumber].traderQuantity = masterList.itemMasterList [itemNumber].traderQuantity - 1;
			if (masterList.itemMasterList [itemNumber].traderQuantity < 1){
				masterList.itemMasterList [itemNumber].traderOwns = false;
			}
		}
	}


	public void equipClick(){


		itemNumber = currentItem.itemNumber;
		Debug.Log (masterList.itemMasterList [itemNumber].itemNumber + " current item number");

		//checking if headgear when equipping
		if (string.Equals (masterList.itemMasterList [itemNumber].itemType, "headgear")) {
			//check if there is a headgear already equipped
			alreadyEquippedItem = GameObject.FindGameObjectWithTag ("headgear");
			if (alreadyEquippedItem != null) {
				//then it exsists and we should make a local copy to turn it off and destroy
				EquippedItemPopField = alreadyEquippedItem.GetComponent<UseItemToPopulateFields> ();
				InvItem equippedItem = masterList.itemMasterList [EquippedItemPopField.itemNumber];
				equippedItem.isWearing = false;
				equippedItem.isInInventory = true;

				//grabbing item to be equipped and setting local bools
				bool wearable = masterList.itemMasterList [itemNumber].wearable;
				bool iswearing = masterList.itemMasterList [itemNumber].isWearing;
				//if wearable is true, set iswearing to true
				if (wearable) {
					if (iswearing) {
						masterList.itemMasterList [itemNumber].isInInventory = true;
						masterList.itemMasterList [itemNumber].isWearing = false;
						Debug.Log (masterList.itemMasterList [itemNumber].title + " should not be equipped");

					} else {
						masterList.itemMasterList [itemNumber].isWearing = true;
						masterList.itemMasterList [itemNumber].isInInventory = false;
						Debug.Log (masterList.itemMasterList [itemNumber].title + " should be equipped");

					} 

				}
			}
			if (alreadyEquippedItem == null) {
				//no need to take out an equipped item if there isn't one
				//grabbing item to be equipped and setting local bools
				bool wearable = masterList.itemMasterList [itemNumber].wearable;
				bool iswearing = masterList.itemMasterList [itemNumber].isWearing;
				//if wearable is true, set iswearing to true
				if (wearable) {
					if (iswearing) {
						masterList.itemMasterList [itemNumber].isInInventory = true;
						masterList.itemMasterList [itemNumber].isWearing = false;
						Debug.Log (masterList.itemMasterList [itemNumber].title + " should not be equipped");

					} else {
						masterList.itemMasterList [itemNumber].isWearing = true;
						masterList.itemMasterList [itemNumber].isInInventory = false;
						Debug.Log (masterList.itemMasterList [itemNumber].title + " should be equipped");

					} 

				}
			}



		}



	



	}

	public void unequipClick(){
		itemNumber = currentItem.itemNumber;
		//itemNumber = itemNumber - 1;

		//check that it is equipped, and then unequip it and put it back into the inventory unless the inventory is full
		bool isWearing = masterList.itemMasterList[itemNumber].isWearing;
		if (isWearing){
			masterList.itemMasterList [itemNumber].isWearing = false;
			masterList.itemMasterList [itemNumber].isInInventory = true;
			Debug.Log (masterList.itemMasterList[itemNumber].title +" should be unequipped");

		}
	}

	public void dropItem(){
		itemNumber = currentItem.itemNumber;
		//itemNumber = itemNumber - 1;

		bool isInInventory = masterList.itemMasterList [itemNumber].isInInventory;
		bool isWearing = masterList.itemMasterList [itemNumber].isWearing; 
		if (isInInventory || isWearing){
			masterList.itemMasterList [itemNumber].isInInventory = false;
			masterList.itemMasterList [itemNumber].isWearing = false;
			masterList.itemMasterList [itemNumber].quantity = 1;
			Debug.Log (masterList.itemMasterList[itemNumber].title +" should be dropped");

		}
	}
}
