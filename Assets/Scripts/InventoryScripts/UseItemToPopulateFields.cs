using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class UseItemToPopulateFields : MonoBehaviour {

	public int itemNumber;
	public Text title;
	public Text flavorText;
	public Text cost;
	public Image icon;
	public Image popupIcon;
	public Image gearIcon;
	public Text quantity;
	
	private ItemMaster itemMaster;

	void Start(){
		itemMaster = FindObjectOfType<ItemMaster> ();

		PopulateInfo();
		PopulateGear ();
	}


	public void PopulateInfo()
	{
		//print (itemNumber);
		title.text = itemMaster.itemMasterList [itemNumber].title;
		flavorText.text = itemMaster.itemMasterList [itemNumber].flavorText;
		icon.sprite = itemMaster.itemMasterList [itemNumber].icon;
		popupIcon.sprite = itemMaster.itemMasterList [itemNumber].icon;
		quantity.text = itemMaster.itemMasterList [itemNumber].quantity.ToString();




	}

	public void PopulateGear(){
		if(string.Equals(itemMaster.itemMasterList [itemNumber].itemType, "headgear")){
			if(itemMaster.itemMasterList[itemNumber].isWearing){
				gearIcon.sprite = itemMaster.itemMasterList [itemNumber].icon;
				Debug.Log ( itemMaster.itemMasterList [itemNumber].title + " should show in gear");
			}

		}
		if(string.Equals(itemMaster.itemMasterList [itemNumber].itemType, "footgear")){
			if(itemMaster.itemMasterList[itemNumber].isWearing){
				gearIcon.sprite = itemMaster.itemMasterList [itemNumber].icon;
				Debug.Log ( itemMaster.itemMasterList [itemNumber].title + " should show in gear");
			}

		}
		if(string.Equals(itemMaster.itemMasterList [itemNumber].itemType, "scroll")){
			if(itemMaster.itemMasterList[itemNumber].isWearing){
				gearIcon.sprite = itemMaster.itemMasterList [itemNumber].icon;
				Debug.Log ( itemMaster.itemMasterList [itemNumber].title + " should show in gear");
			}

		}
		if(string.Equals(itemMaster.itemMasterList [itemNumber].itemType, "pickaxe")){
			if(itemMaster.itemMasterList[itemNumber].isWearing){
				gearIcon.sprite = itemMaster.itemMasterList [itemNumber].icon;
				Debug.Log ( itemMaster.itemMasterList [itemNumber].title + " should show in gear");
			}

		}
	}
}