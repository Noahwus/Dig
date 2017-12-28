using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class Equip_Unequip : MonoBehaviour, IPointerClickHandler {

	private PopulateInventoryListFromItemsMaster currentInventory;
	public int itemNumber;
	private UseItemToPopulateFields currentItem;
	private ItemMaster masterList;

	public UnityEvent leftClick;
	public UnityEvent middleClick;
	public UnityEvent rightClick;


	void Awake(){
		currentInventory = FindObjectOfType<PopulateInventoryListFromItemsMaster> ();
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


	public void equipClick(){


		itemNumber = currentItem.itemNumber;
		Debug.Log (masterList.itemMasterList [itemNumber].itemNumber + "current item number");



		//set local wearable bool
		bool wearable = masterList.itemMasterList[itemNumber].wearable;
		bool iswearing = masterList.itemMasterList[itemNumber].isWearing;
		//if wearable is true, set iswearing to true
		if (wearable) {
			if (iswearing) {
				masterList.itemMasterList [itemNumber].isInInventory = true;
				masterList.itemMasterList[itemNumber].isWearing = false;
				Debug.Log (masterList.itemMasterList[itemNumber].title +" should not be equipped");

			} else {
				masterList.itemMasterList[itemNumber].isWearing = true;
				masterList.itemMasterList [itemNumber].isInInventory = false;
				Debug.Log (masterList.itemMasterList[itemNumber].title +" should be equipped");

			} 

		}
	}

	public void unequipClick(){
		itemNumber = currentItem.itemNumber;
		itemNumber = itemNumber - 1;

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
		itemNumber = itemNumber - 1;

		bool isInInventory = masterList.itemMasterList [itemNumber].isInInventory;
		if (isInInventory){
			masterList.itemMasterList [itemNumber].isInInventory = false;
			Debug.Log (masterList.itemMasterList[itemNumber].title +" should be dropped");

		}
	}
}
