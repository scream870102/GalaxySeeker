using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : Character {
    protected PlayerStats stats;
    public PlayerStats Stats { get { return stats; } }
    protected PlayerMovement movement;
    //need to add some logic for state
    public string State;
    private void Awake ( ) {
        stats.OnHealthReachedZero += Dead;
        movement = GetComponent<PlayerMovement> ( );
        movement.Parent = this;

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

}
