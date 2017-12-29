using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGearFromItemMaster : MonoBehaviour {

	private ItemMaster masterList;

	//instance to make
	public GameObject gear;
	//possible parents of instance
	public GameObject headGearLocator;
	public GameObject footGearLocator;
	public GameObject pickAxeLocator;
	public GameObject scrollLocator;

	float updateGearTime = 0.01f;

	void Awake(){
		masterList = FindObjectOfType<ItemMaster> ();
	}

	void OnEnable(){
		StartCoroutine (updateGearVisuals());
	}

	IEnumerator updateGearVisuals(){
		while (enabled){
			updateGear ();
			yield return new WaitForSeconds (updateGearTime);
		}
	}

	void updateGear(){
		for (int i = 0; i < masterList.itemMasterList.Count; i++) {
			InvItem myItem = masterList.itemMasterList [i];
			if(GameObject.Find(myItem.name + "equipped" + myItem.itemType) != null){
				//means there is one there
				//check that it should be there
				if(myItem.isWearing){
					//should be here
				}else {
					//shouldn't be here, destroy instance
					Destroy(GameObject.Find(myItem.name + "equipped" + myItem.itemType));
					Debug.Log (myItem.title + " instance was destroyed");	
				}
			}
			if(GameObject.Find(myItem.name + "equipped" + myItem.itemType) == null){
				//instance doesn't exist. check if it should
				if(myItem.isWearing){
					//should be here
					//create the instance
					if (string.Equals (myItem.itemType, "headgear")) {
						var clone = Instantiate (gear, headGearLocator.transform);
						clone.name = myItem.name + "equipped" + myItem.itemType;
						clone.tag = "headgear";
						UseItemToPopulateFields popfield = clone.GetComponent<UseItemToPopulateFields> ();
						popfield.itemNumber = myItem.itemNumber;
					}
					if (string.Equals ( myItem.itemType, "footgear")) {
						var clone = Instantiate (gear, footGearLocator.transform);
						clone.name = myItem.name + "equipped"+ myItem.itemType;
						clone.tag = "footgear";
						UseItemToPopulateFields popfield = clone.GetComponent<UseItemToPopulateFields> ();
						popfield.itemNumber = myItem.itemNumber;
					}
					if (string.Equals ( myItem.itemType, "scroll")) {
						var clone = Instantiate (gear, scrollLocator.transform);
						clone.name = myItem.name + "equipped"+ myItem.itemType;
						clone.tag = "scroll";
						UseItemToPopulateFields popfield = clone.GetComponent<UseItemToPopulateFields> ();
						popfield.itemNumber = myItem.itemNumber;
					}
					if (string.Equals ( myItem.itemType, "pickaxe")) {
						var clone = Instantiate (gear, pickAxeLocator.transform);
						clone.name = myItem.name + "equipped"+ myItem.itemType;
						clone.tag = "pickaxe";
						UseItemToPopulateFields popfield = clone.GetComponent<UseItemToPopulateFields> ();
						popfield.itemNumber = myItem.itemNumber;
					}
				}else {
					//shouldn't be here so we're good
				}
			}

		}
	}
}
