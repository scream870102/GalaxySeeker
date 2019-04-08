using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[RequireComponent (typeof (Collider2D))]
public class ItemPickUp : MonoBehaviour, IInteractable {
    [SerializeField]
    Item item = null;
    Collider2D col = null;
    // Start is called before the first frame update
    private void Awake ( ) {
        col = GetComponent<Collider2D> ( );
    }
    private void OnTriggerStay2D (Collider2D other) {
        if (other.gameObject.tag == "Player" && Input.GetButtonDown ("Interact")) {
            Interact ( );
        }
    }
    public void Interact ( ) {
        Debug.Log ("Interacting with"+item.name);
    }
}
