using System.Collections;
using System.Collections.Generic;

using UnityEngine;
///<summary>class to manage about player equipment</summary>
///<remarks> inherit from playerComponent</remarks>
public class PlayerEquipment : PlayerComponent {
    // what items in player equipment
    [SerializeField]
    protected List<Item> items = new List<Item> ( );
    // if player can use item right now
    protected bool bItemCanUse;
    private void Start ( ) {
        bItemCanUse = true;
    }

    //need to define how to use item 
    override protected void Update ( ) {
        base.Update ( );
        if (Input.GetKeyDown (KeyCode.Z) && bItemCanUse) {
            items [0].Use ( );
            bItemCanUse = false;
        }
    }
    ///<summary>add a new item into equipment</summary>
    ///<remarks>will get false when there isn't more space in equipment</remarks>
    //must add item to player as a new component then init all the thing about new item
    public bool AddItem (ref Item item) {
        if (items.Count >= Parent.Stats.itemSpace.Value)
            return false;
        Item newItem = gameObject.AddComponent (item.GetType ( )) as Item;
        InitNewItem (ref newItem, ref item);
        items.Add (newItem);
        return true;
    }

    //call back method for event OnItemUsed
    //when item already used,item will invoke event then player can use next item or reuse the same item
    protected void OnItemAlreadyUsed (string name) {
        Debug.Log (name + " is already used");
        bItemCanUse = true;
    }

    ///<summary>remove an item from inventory</summary>
    public void RemoveItem (Item item) {
        items.Remove (item);
    }

    // set item name icon and set regist event
    void InitNewItem (ref Item newItem, ref Item oldItem) {
        newItem.name = oldItem.name;
        newItem.icon = oldItem.icon;
        newItem.OnItemUsed += OnItemAlreadyUsed;
        newItem.Inventory = this;
    }
}
