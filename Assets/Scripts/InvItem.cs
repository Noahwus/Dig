using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/ Item")]

public class InvItem : ScriptableObject {

	public int itemNumber;
	public string title = "New Item";
	public string itemType = "null";
	public int quantity = 1;
	public Sprite icon = null;
	public string flavorText = "boo I'm new";
	public float cost = 110;
	public bool isInInventory = false;
	public bool isBought = false;
	public bool isWearing = false;
	public bool wearable = false;




}
