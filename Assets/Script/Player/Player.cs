using System.Collections.Generic;

using UnityEngine;
/// <summary>Class is top class of player</summary>
public class Player : Character {
    Animator anim = null;
    Rigidbody2D rb = null;
    [SerializeField] PlayerProps props = null;
    PlayerMovement movement = null;
    PlayerEquipment equipment = null;
    PlayerShooting shooting = null;
    //ref for all PlayerComponent
    List<PlayerComponent> components = new List<PlayerComponent> ( );
    public Rigidbody2D Rb { get { return rb; } }
    public Animator Anim { get { return anim; } }
    public PlayerProps Props { get { return props; } }
    public bool IsFacingRight { get { return movement.IsFacingRight; } }
    /// <summary>Define player is swinging with rope right now</summary>
    public bool IsSwing { set { movement.IsSwing = value; } }
    /// <summary>if player is flying state right now</summary>
    public bool IsFlying { set { movement.IsFlying = value; } }
    /// <summary>properry to access the rope hookPoint it will be called when rope cast something</summary>
    public Vector2 HookPoint { set { movement.HookPoint = value; } }
    public Vector2 Velocity { get { return rb.velocity; } set { rb.velocity = value; } }
    public bool IsOnGround { get { return movement.IsGround; } }
    //Get all ref when player Awake
    //Add Dead function to OnHealthReachedZero
    void Awake ( ) {
        Stats.Init ( );
        Stats.OnHealthReachedZero += Dead;
        SetComponent<PlayerMovement> (ref movement);
        SetComponent<PlayerEquipment> (ref equipment);
        SetComponent<PlayerShooting> (ref shooting);
        anim = GetComponent<Animator> ( );
        rb = GetComponent<Rigidbody2D> ( );

    }

    //method to init all playerComponent
    void SetComponent<T> (ref T component) where T : PlayerComponent {
        component = GetComponent<T> ( );
        component.Parent = this;
        components.Add (component);
    }

    //callback function will call when player health reached zero
    void Dead ( ) {
        Anim.SetTrigger ("Dead");
        Debug.Log ("already Dead");
    }
    public override void TakeDamage (float damage) {
        base.TakeDamage (damage);
        Anim.SetTrigger ("TakeDamage");
    }

    /// <summary>add a new item to equipment</summary>
    ///<remarks>will get false when there isn't more space in equipment</remarks>
    public bool AddItem (ref Item item) {
        return equipment.AddItem (ref item);
    }

    /// <summary>add some force to player RigidBody2d </summary>
    public void AddForce (Vector2 force, ForceMode2D mode = ForceMode2D.Impulse) {
        Rb.AddForce (force, mode);
    }

    /// <summary>Enable or Disable all PlayerComponents on Player</summary>
    public void EnableComponents (bool enable) {
        foreach (PlayerComponent component in components)
            component.enabled = enable;
    }

    /// <summary>Enable or Disable Specific PlayerComponent on Player</summary>
    public void EnableComponent<T> (bool enable) where T : PlayerComponent {
        System.Type tmp = typeof (T);
        foreach (PlayerComponent component in components) {
            System.Type tem = component.GetType ( );
            if (tmp.Equals (tem))
                component.enabled = enable;
        }
    }
}

/// <summary>define the property player will needed</summary>
[System.Serializable]
public class PlayerProps {
    ///<summary>define how fast does player walk on the ground</summary>
    public float WalkSpeed;
    ///<summary>define how many force will add when player jump</summary>
    public float JumpForce;
    ///<summary>define how fast dose player move when player isn't on the ground</summary>
    public float AirSpeed;
    ///<summary>how many item can player equip</summary>
    public int ItemSpace;
    ///<summary> define how many force will add to player when player is swing with rope
    public float SwingForce;
    /// <summary> define how many force will add to player when player is flying with jetPack</summary>
    public float FlyingGasForce;
    /// <summary>the basic damage of player</summary>
    public float Damage;
}
