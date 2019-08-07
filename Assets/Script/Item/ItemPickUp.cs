using GalaxySeeker.Interface;

using UnityEngine;
/// <summary>item on map and can interact with player</summary>
[RequireComponent (typeof (Collider2D), typeof (Item))]
public class ItemPickUp : MonoBehaviour, IInteractable {
    //what is this item
    Item item = null;
    //collider for this item 
    public Collider2D col = null;
    //owner of this item
    Player owner = null;
    void Awake ( ) {
        col = GetComponent<Collider2D> ( );
        item = GetComponent<Item> ( );
    }

    //if player press interact button then item will be picked up
    void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown ("Interact")) {
            col.enabled = false;
            owner = other.GetComponent<Player> ( );
            Interact ( );
        }
    }

    /// <summary> if this function been call then item will add to owner's inventory then gameObject will being Destroy</summary>
    public void Interact ( ) {
        if (owner == null)
            return;
        owner.AddItem (ref item);
    }
}
