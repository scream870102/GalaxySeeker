using System.Collections;
using System.Collections.Generic;

using UnityEngine;
///<summary>class to manage about player equipment</summary>
///<remarks> inherit from playerComponent</remarks>
public class PlayerEquipment : PlayerComponent {
    // what items in player equipment
    protected List<Item> items = new List<Item> ( );

    override protected void Update ( ) {
        base.Update ( );
    }
    ///<summary>add a new item into equipment</summary>
    ///<remarks>will get false when there isn't more space in equipment</remarks>
    public bool AddItem (Item item) {
        if (items.Count >= Parent.Stats.itemSpace.Value)
            return false;
        items.Add (item);
        item.Inventory = this;
        return true;
    }
    ///<summary>remove an item from inventory</summary>
    public void RemoveItem (Item item) {
        items.Remove (item);
    }
}
