using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
///<summary>class to manage about player equipment</summary>
///<remarks> inherit from playerComponent</remarks>
public class PlayerEquipment : PlayerComponent {
    // what items in player equipment
    [SerializeField]
    protected List<Item> items = new List<Item> ( );
    // field to keep tracing player current item index
    protected int currentItemIndex;
    // field if player using some item now
    protected bool bItemUsing;
    // property to access bItmeUsing write only
    public bool IsItemUsing { set { bItemUsing = value; } }
    // if player can use item right now
    //protected bool bItemCanUse;
    private void Start ( ) {
        currentItemIndex = 0;
        bItemUsing = false;
    }

    //need to define how to use item 
    override protected void Update ( ) {
        base.Update ( );
        SwitchItem ( );
    }
    ///<summary>add a new item into equipment</summary>
    ///<remarks>will get false when there isn't more space in equipment</remarks>
    //must add item to player as a new component then init all the thing about new item
    public bool AddItem (ref Item item) {
        if (items.Count >= Parent.Stats.itemSpace.Value)
            return false;
        GameObject itemGameObject = GameObject.Instantiate (item.itemGameObject);
        if (itemGameObject != null) {
            itemGameObject.transform.parent = this.transform;
            Item newItem = itemGameObject.GetComponent<Item> ( );
            InitNewItem (ref newItem, ref item);
            items.Add (newItem);
            items [currentItemIndex].IsItemCanUse = true;
            return true;
        }
        return false;
    }

    //call back method for event OnItemUsed
    //when item already used,item will invoke event then player can use next item or reuse the same item
    protected void OnItemAlreadyUsed (string name) {
        bItemUsing = false;
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
        Vector3 initPos = new Vector3 (.0f, .0f, .0f);
        newItem.transform.localPosition = initPos;
    }

    //define if player switch item button how item switch
    //only can switch item when player isn't using any item 
    void SwitchItem ( ) {
        if (Input.GetButtonDown ("Switch") && !bItemUsing) {
            items [currentItemIndex].IsItemCanUse = false;
            if (currentItemIndex < items.Count - 1)
                currentItemIndex++;
            else
                currentItemIndex = 0;
            items [currentItemIndex].IsItemCanUse = true;
        }

    }

}
