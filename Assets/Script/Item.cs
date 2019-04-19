using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>class define what an item has</summary>
/// <remarks>inclue icon name and which inventory belongs</remarks>
[CreateAssetMenu (fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {
	//field for player inventory
	protected PlayerEquipment inventory = null;
	/// <summary>which inventory of this item belongs to</summary>
	public PlayerEquipment Inventory { set { if (inventory == null) inventory = value; } }
	/// <summary>item's name</summary>
	new public string name = "New Item";
	/// <summary> item icon</summary>
	public Sprite icon = null;

	/// <summary> Called when the item is being used</summary>
	public virtual void Use ( ) {
		// Use the item
		// Something may happen
	}

	/// <summary>Item is being used and going to remove from hierachy</summary>
	public void RemoveFromInventory ( ) {
		if (inventory == null) {
			Debug.LogWarning ("No Inventory");
			return;
		}
		inventory.RemoveItem (this);
		Destroy(this);
	}

}
