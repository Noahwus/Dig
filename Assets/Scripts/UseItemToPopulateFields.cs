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
	
	private ItemMaster itemMaster;

	void Start(){
		itemMaster = FindObjectOfType<ItemMaster> ();

		PopulateInfo();
	}


	public void PopulateInfo()
	{
		//print (itemNumber);
		title.text = itemMaster.itemMasterList [itemNumber].title;
		flavorText.text = itemMaster.itemMasterList [itemNumber].flavorText;
		icon.sprite = itemMaster.itemMasterList [itemNumber].icon;
		popupIcon.sprite = itemMaster.itemMasterList [itemNumber].icon;




	}

	public void PopulateGear(){
		if(string.Equals(itemMaster.itemMasterList [itemNumber].itemType, "headgear")){
		gearIcon.sprite = itemMaster.itemMasterList [itemNumber].icon;
		}
	}
}