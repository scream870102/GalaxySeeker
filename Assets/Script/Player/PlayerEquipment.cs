using System.Collections.Generic;

using UnityEngine;
///<summary>class to manage about player equipment</summary>
///<remarks> inherit from playerComponent</remarks>
public class PlayerEquipment : PlayerComponent {
    // what items in player equipment
    [SerializeField]
    List<Item> items = new List<Item> ( );
    // field to keep tracing player current item index
    int currentItemIndex;
    // field if player using some item now
    bool bItemUsing;
    // property to access bItemUsing write only
    public bool IsItemUsing { set { bItemUsing = value; } }
    // if player can use item right now
    //protected bool bItemCanUse;
    void Start ( ) {
        currentItemIndex = 0;
        bItemUsing = false;
    }

    //need to define how to use item 
    protected override void Tick ( ) {
        SwitchItem ( );
    }
    ///<summary>add a new item into equipment</summary>
    ///<remarks>will get false when there isn't more space in equipment</remarks>
    public bool AddItem (ref Item item) {
        if (items.Count >= Parent.Props.ItemSpace)
            return false;
        ItemPickUp pickUpComponent = item.gameObject.GetComponent<ItemPickUp> ( );
        if (pickUpComponent != null) {
            Collider2D col = pickUpComponent.col;
            Destroy (pickUpComponent);
            Destroy (col);
            item.transform.parent = this.gameObject.transform;
            InitNewItem (ref item);
            items.Add (item);
            items [currentItemIndex].IsItemCanUse = true;
            return true;
        }
        return false;
    }

    //call back method for event OnItemUsed
    //when item already used,item will invoke event then player can use next item or reuse the same item
    void OnItemAlreadyUsed (string name) {
        bItemUsing = false;
    }

    ///<summary>remove an item from inventory</summary>
    public void RemoveItem (Item item) {
        items.Remove (item);
    }
    //when a new item is going to be add to inventory call this method
    //subscribe OnItemUsed event
    //set localPosition and set sprite
    void InitNewItem (ref Item item) {
        item.OnItemUsed += OnItemAlreadyUsed;
        item.Inventory = this;
        item.transform.localPosition = item.InitPos;
        item.spriteRenderer.sprite = item.icon;
    }

    //define if player switch item button how item switch
    //only can switch item when player isn't using any item 
    void SwitchItem ( ) {
        if (Input.GetButtonDown ("Switch") && !bItemUsing && items.Count > 0) {
            items [currentItemIndex].IsItemCanUse = false;
            if (currentItemIndex < items.Count - 1)
                currentItemIndex++;
            else
                currentItemIndex = 0;
            items [currentItemIndex].IsItemCanUse = true;
        }

    }

}
