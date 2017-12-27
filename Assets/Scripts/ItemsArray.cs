using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsArray: MonoBehaviour {


	public InvItem[] itemsArray;

	void Awake(){
		DontDestroyOnLoad (transform.gameObject);
	}

}
