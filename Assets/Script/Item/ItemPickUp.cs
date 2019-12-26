using UnityEngine;
namespace GalaxySeeker.Item {
    /// <summary>item on map and can interact with player</summary>
    [RequireComponent (typeof (Collider2D), typeof (Item))]
    public class ItemPickUp : MonoBehaviour, IInteractable {
        Item item = null;
        public Collider2D col = null;
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
}
