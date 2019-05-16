using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>Class is top class of player</summary>
public class Player : Character {
    [SerializeField]
    //all stats of player include hp shield attackpoint and some constants
    protected PlayerStats stats;
    /// <summary>all player stats include all constants or some variables</summary>
    public PlayerStats Stats { get { return stats; } }
    //ref of playerMovement
    protected PlayerMovement movement = null;
    //ref of Playerequipment
    protected PlayerEquipment equipment = null;
    /// <summary> if player now facing right direction?</summary>
    public bool IsPlayerFacingRight { get { return movement.IsPlayerFacingRight; } }
    /// <summary>State define current player move state then animator change according to this
    //need to add some logic for state
    public string State;
    public bool IsPlayerSwing { set { movement.IsPlayerSwing = value; } }
    public Vector2 HookPoint { set { movement.HookPoint = value; } }
    //Get all ref when player Awake
    //Add Dead function to OnHealthReachedZero
    private void Awake ( ) {
        stats.OnHealthReachedZero += Dead;
        SetComponent<PlayerMovement> (ref movement);
        SetComponent<PlayerEquipment> (ref equipment);
    }

    //method to init all playerComponent
    void SetComponent<T> (ref T component) where T : PlayerComponent {
        component = GetComponent<T> ( );
        component.Parent = this;
    }

    //callback function will call when player health reached zero
    void Dead ( ) {
        Debug.Log (stats.name + "already Dead");
    }

    /// <summary>add a new item to equipment</summary>
    ///<remarks>will get false when there isn't more space in equipment</remarks>
    public bool AddItem (ref Item item) {
        return equipment.AddItem (ref item);
    }

    public void AddForce (Vector2 force, ForceMode2D mode = ForceMode2D.Impulse) {
        movement.AddForce (force, mode);
    }

}
