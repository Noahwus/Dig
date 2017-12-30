using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class BlockClickingLogic : MonoBehaviour, IPointerClickHandler{

	public UnityEvent leftClick;



	public void OnPointerClick(PointerEventData eventData){
		if (eventData.button == PointerEventData.InputButton.Left)
			leftClick.Invoke ();
	}


	public void blockClick(){
		Debug.Log(gameObject.name);
	}
}
