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
			if(GameObject.Find(myItem.name + "equipped") != null){
				//means there is one there
				//check that it should be there
				if(myItem.isWearing){
					//should be here
				}else {
					//shouldn't be here, destroy instance
					Destroy(GameObject.Find(myItem.name + "equipped"));
				}
			}
			if(GameObject.Find(myItem.name + "equipped") == null){
				//instance doesn't exist. check if it should
				if(myItem.isWearing){
					//should be here
					//create the instance
					var clone = Instantiate(gear, headGearLocator.transform);
					clone.name = myItem.name + "equipped";
					UseItemToPopulateFields popfield = clone.GetComponent<UseItemToPopulateFields> ();
					popfield.itemNumber = myItem.itemNumber;
				}else {
					//shouldn't be here so we're good
				}
			}

		}
	}
}
