using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>class define what an item has</summary>
/// <remarks>inclue icon name and which inventory belongs</remarks>
public class Item : MonoBehaviour {
	[SerializeField]
	//field for player inventory
	protected PlayerEquipment inventory = null;
	/// <summary>which inventory of this item belongs to</summary>
	public PlayerEquipment Inventory { set { if (inventory == null) inventory = value; } }
	/// <summary>item's name</summary>
	new public string name = "New Item";
	/// <summary> item icon</summary>
	public Sprite icon = null;
	public event System.Action<string> OnItemUsed;
	protected bool bItemUsing;

	private void OnEnable ( ) {
		bItemUsing = false;
		Reset ( );
	}

	/// <summary> Called when the item is being used</summary>
	public void Use ( ) {
		bItemUsing = true;

	}

	protected void Update ( ) {
		if (!bItemUsing)
			return;
		UsingItem ( );
	}

	protected virtual void UsingItem ( ) {
		// Use the item
		// Something may happen
		//must call AlreadyUsed when finsh using item
	}

	public void AlreadUsed ( ) {
		bItemUsing = false;
		Reset ( );
		if (OnItemUsed != null)
			OnItemUsed (name);
	}

	protected virtual void Reset ( ) { }

	/// <summary>Item is being used and going to remove from hierachy</summary>
	public void RemoveFromInventory ( ) {
		if (inventory == null) {
			Debug.LogWarning ("No Inventory");
			return;
		}
		inventory.RemoveItem (this);
		Destroy (this);
	}

}
