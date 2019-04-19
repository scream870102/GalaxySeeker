using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : Character {
    protected PlayerStats stats;
    public PlayerStats Stats { get { return stats; } }
    protected PlayerMovement movement = null;
    protected PlayerEquipment equipment = null;
    public bool IsPlayerFacingRight { get { return movement.IsPlayerFacingRight; } }
    //need to add some logic for state
    public string State;
    private void Awake ( ) {
        stats.OnHealthReachedZero += Dead;
        SetComponent<PlayerMovement> (movement);
        SetComponent<PlayerEquipment> (equipment);

    }
    // Start is called before the first frame update
    void Start ( ) {

    }

    // Update is called once per frame
    void Update ( ) {

    }

    void Dead ( ) {
        Debug.Log (stats.name + "already Dead");
    }

    void SetComponent<T> (T component) where T : PlayerComponent {
        component = GetComponent<T> ( );
        component.Parent = this;
    }

    public void AddItem (Item item) {
        equipment.AddItem (item);
    }

}
