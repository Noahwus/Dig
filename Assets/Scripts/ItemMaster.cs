using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemMaster: MonoBehaviour {


	public List<InvItem> itemMasterList;

	void Start(){

	}

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
//		Debug.Log (itemMasterList.Count);

	}


}