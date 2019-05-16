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
	public PlayerEquipment Inventory { set { if (inventory == null) { inventory = value; Init ( ); owner = inventory.Parent; } } }
	/// <summary>which gameObject need to initialize when item being taken by player
	public GameObject itemGameObject;
	/// <summary>item's name</summary>
	new public string name = "New Item";
	/// <summary> item icon</summary>
	public Sprite icon = null;
	/// <summary>if Item alreay used this event will invoke</summary>
	/// <param name="string">item name</param>
	public event System.Action<string> OnItemUsed;
	// //if Item is using now
	// protected bool bItemUsing;
	protected bool bItemCanUse;
	public bool IsItemCanUse { set { bItemCanUse = value; } }
	protected SpriteRenderer spriteRenderer;
	protected Player owner;

	//when enable reset Item
	private void OnEnable ( ) {
		bItemCanUse = false;
		spriteRenderer = GetComponent<SpriteRenderer> ( );
	}

	//if item state is using keep call UsingItem()
	protected void Update ( ) {
		if (!bItemCanUse)
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
		Reset ( );
		if (OnItemUsed != null)
			OnItemUsed (name);
	}
	/// <summary>Item is being used and going to remove from hierachy</summary>
	public void RemoveFromInventory ( ) {
		if (inventory == null) {
			Debug.LogWarning ("No Inventory");
			return;
		}
		inventory.RemoveItem (this);
		RemoveItem ( );
		Destroy (this);
	}

	//define reset action 
	//child class can override this method to add something need to reset
	protected virtual void Reset ( ) { }

	protected virtual void RemoveItem ( ) { }

	protected virtual void Init ( ) { spriteRenderer.enabled = false; }

}
