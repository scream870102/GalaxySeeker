using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class SpargeShoes : Item {
    /// <summary>how many force will add to player when player use spargeShoes</summary>
    public int spargingForce;
    /// <summary>how long between two times using shoes</summary>
    public float coolDown;
    //if shoes can use right now
    private bool bCanUse;
    //timer for cd
    private float timer;
    override protected void UsingItem ( ) {
        if (Input.GetButtonDown ("Use") && bCanUse) {
            BeginUsing ( );
            owner.Velocity = new Vector2 (0f, 0f);
            owner.AddForce (new Vector2 (owner.IsFacingRight?spargingForce: -spargingForce, 0f));
            bCanUse = false;
            timer = Time.time + coolDown;
        }
        if (!bCanUse && timer < Time.time) {
            bCanUse = true;
            AlreadUsed ( );
        }
    }
    override protected void Reset ( ) { }
    override protected void Init ( ) {
        sr.enabled = true;
        bCanUse = true;
    }
}
