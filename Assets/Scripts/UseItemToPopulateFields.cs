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
	//private ItemsArray itemArray;

	void Start(){
		//itemArray = FindObjectOfType<ItemsArray> ();

		//PopulateInfo();
	}


	public void PopulateInfo()
	{
		print (itemNumber);
		//title.text =  itemArray.itemsArray [itemNumber].title;
		//flavorText.text = itemArray.itemsArray [itemNumber].flavorText;
		//icon.sprite = itemArray.itemsArray [itemNumber].icon;
		//popupIcon.sprite = itemArray.itemsArray [itemNumber].icon;



	}
}