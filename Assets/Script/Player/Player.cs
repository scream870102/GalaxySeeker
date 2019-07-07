using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>Class is top class of player</summary>
public class Player : Character {
    [SerializeField]
    //all stats of player include hp shield attackpoint and some constants
    new PlayerStats stats;
    /// <summary>all player stats include all constants or some variables</summary>
    public new PlayerStats Stats { get { return stats; } }
    //ref of playerMovement
    PlayerMovement movement = null;
    //ref of Playerequipment
    PlayerEquipment equipment = null;
    //ref of Playershooting
    PlayerShooting shooting = null;
    //ref for all PlayerComponent
    List<PlayerComponent> components;
    /// <summary> if player now facing right direction?</summary>
    public bool IsFacingRight { get { return movement.IsFacingRight; } }
    /// <summary>State define current player move state then animator change according to this
    //need to add some logic for state
    public string State;
    // property to access movement is playerSwing
    public bool IsSwing { set { movement.IsSwing = value; } }
    //properry to access the rope hookPoint it will be called when rope cast something
    public Vector2 HookPoint { set { movement.HookPoint = value; } }
    /// <summary> if player standing on ground</summary>
    public bool IsOnGround { get { return movement.IsGround; } }
    /// <summary>Get rigidbody2d velocity</summary>
    public Vector2 Velocity { get { return movement.Velocity; } set { movement.Velocity = value; } }
    /// <summary>if player is flying state right now</summary>
    public bool IsFlying { set { movement.IsFlying = value; } }
    //Get all ref when player Awake
    //Add Dead function to OnHealthReachedZero
    void Awake ( ) {
        stats.Init ( );
        stats.OnHealthReachedZero += Dead;
        components = new List<PlayerComponent> ( );
        SetComponent<PlayerMovement> (ref movement);
        SetComponent<PlayerEquipment> (ref equipment);
        SetComponent<PlayerShooting> (ref shooting);
    }

    //method to init all playerComponent
    void SetComponent<T> (ref T component) where T : PlayerComponent {
        component = GetComponent<T> ( );
        component.Parent = this;
        components.Add (component);
    }

    //callback function will call when player health reached zero
    void Dead ( ) {
        Debug.Log ("already Dead");
    }

    /// <summary>add a new item to equipment</summary>
    ///<remarks>will get false when there isn't more space in equipment</remarks>
    public bool AddItem (ref Item item) {
        return equipment.AddItem (ref item);
    }
    /// <summary>add some force to player RigidBody2d </summary>
    public void AddForce (Vector2 force, ForceMode2D mode = ForceMode2D.Impulse) {
        movement.AddForce (force, mode);
    }

    /// <summary>Enable or Disable all PlayerComponents on Player</summary>
    public void EnableComponents (bool enable) {
        foreach (PlayerComponent component in components) {
            component.enabled = enable;
        }
    }

    /// <summary>Enable or Disable Specific PlayerComponent on Plyaer</summary>
    public void EnableComponent<T> (bool enable) where T : PlayerComponent {
        System.Type tmp = typeof (T);
        foreach (PlayerComponent component in components) {
            System.Type tem = component.GetType ( );
            if (tmp.Equals (tem)) {
                component.enabled = enable;
            }

        }
    }

}
