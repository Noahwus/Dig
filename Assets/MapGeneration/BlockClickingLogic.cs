using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BlockClickingLogic : MonoBehaviour{

	GameObject tempPlayer;

	private PlayerController playerController;


	void FixedUpdate(){
			
	}

	void OnCollisionEnter2D(Collision2D transformCollision){
		//collided with something new
		Debug.Log("collided with something");
		if(transformCollision.gameObject.tag == "Player"){
			//say we collided with player
			Debug.Log("collided with player");

			tempPlayer = transformCollision.gameObject;

			//get local ref of the player controller script
			playerController = tempPlayer.GetComponent<PlayerController> ();

			//check if player is holding mouse down  
			if (playerController.isLeftClicking){
				Debug.Log ("they are left clicking");
				Destroy (gameObject, 1f);
				//check if they are close enough
				//destroy the object after a wait time for the animation
				//create droppables
			}

		}
		
	}


}
