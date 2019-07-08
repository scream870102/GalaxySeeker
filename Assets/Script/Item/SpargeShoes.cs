using UnityEngine;

public class SpargeShoes : Item {
    /// <summary>how many force will add to player when player use spargeShoes</summary>
    public int spargingForce;
    /// <summary>how long between two times using shoes</summary>
    public float coolDown;
    //if shoes can use right now
    bool bCanUse;
    //timer for cd
    float timer;
    //ref for ParticleSystem
    ParticleSystem ptc;
    //ref for ptc shape
    ParticleSystem.ShapeModule ptcShape;
    public float particleYOffset;
    override protected void UsingItem ( ) {
        if (Input.GetButtonDown ("Use") && bCanUse) {
            BeginUsing ( );
            owner.Velocity = new Vector2 (0f, 0f);
            owner.AddForce (new Vector2 (owner.IsFacingRight?spargingForce: -spargingForce, 0f));
            bCanUse = false;
            timer = Time.time + coolDown;
            ptcShape.position = new Vector3 (0f, owner.IsFacingRight? - particleYOffset : particleYOffset, 0f);
            ptc.Play ( );
            Invoke ("StopPTC", 0.5f);
        }
        if (!bCanUse && timer < Time.time) {
            bCanUse = true;
            AlreadyUsed ( );
        }
    }
    override protected void Reset ( ) { }
    override protected void Init ( ) {
        sr.enabled = true;
        bCanUse = true;
        ptc = GetComponent<ParticleSystem> ( );
        ptcShape = ptc.shape;
    }
    //Invoke stop ptc after .5f
    void StopPTC ( ) {
        ptc.Stop ( );
    }
}
