using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerEquipment : PlayerComponent {
    protected List<Item> items = new List<Item> ( );
    void Start ( ) {

    }

    // Update is called once per frame
    override protected void Update ( ) {
        base.Update ( );
    }
    public void AddItem (Item item) {
        if (items.Count >= Parent.Stats.itemSpace.Value)
            return;
        items.Add (item);
        item.Inventory = this;
    }
    public void RemoveItem (Item item) {
        items.Remove (item);
    }
}
