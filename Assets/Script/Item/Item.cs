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
	/// <summary>if Item alreay used this event will invoke</summary>
	/// <param name="string">item name</param>
	public event System.Action<string> OnItemUsed;
	//if Item is using now
	protected bool bItemUsing;

	//when enable reset Item
	private void OnEnable ( ) {
		bItemUsing = false;
		Reset ( );
	}

	/// <summary> Called when the item is being used</summary>
	public void Use ( ) {
		bItemUsing = true;
	}

	//if item state is using keep call UsingItem()
	protected void Update ( ) {
		if (!bItemUsing)
			return;
		UsingItem ( );
	}

	//define action when item is being used update every frame
	//child class must override this method
	protected virtual void UsingItem ( ) {
		// Use the item
		// Something may happen
		//must call AlreadyUsed when finsh using item
	}

	///<summary>if item finish its action call this method to reset all field and invoke event OnItemUsed</summary>
	protected void AlreadUsed ( ) {
		bItemUsing = false;
		Reset ( );
		if (OnItemUsed != null)
			OnItemUsed (name);
	}
	//define reset action 
	//child class can override this method to add something need to reset
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
