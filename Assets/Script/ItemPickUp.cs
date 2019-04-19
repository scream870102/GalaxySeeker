using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>item on map and can interact with player</summary>
[RequireComponent (typeof (Collider2D))]
public class ItemPickUp : MonoBehaviour, IInteractable {
    //what is this item
    [SerializeField]
    Item item = null;
    //collider for this item 
    Collider2D col = null;
    //owner of this item
    Player owner = null;
    private void Awake ( ) {
        col = GetComponent<Collider2D> ( );
    }
    
    //if player press interact button then item will be picked up
    private void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown ("Interact")) {
            owner=other.GetComponent<Player>();
            Interact ( );
        }
    }

    // if this function been call then item will add to owner's inventory then gameObject will being Destroy
    public void Interact ( ) {
        if(owner==null)
            return;
        Debug.Log ("Interacting with" + item.name);
        owner.AddItem(Instantiate(item));
        Destroy(this.gameObject);
        
    }
}
