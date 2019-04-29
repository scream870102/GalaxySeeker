using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[RequireComponent (typeof (Rigidbody2D))]
public class PlayerMovement : PlayerComponent {
    //
    //
    //drag ref
    //
    ///<summary>define which layer is ground</summary>
    public List<LayerMask> groundLayer = new List<LayerMask> ( );
    //
    //
    //const
    //
    //damp for move
    private float smoothDamp = .05f;
    //ref velocity for smooth damp
    private Vector2 refVelocity = Vector2.zero;
    //
    //referrence
    //rigidbody ref
    protected Rigidbody2D rb = null;
    //transform for foot
    public Transform detectGround = null;
    //
    //
    //field
    //
    //bool for jumping state
    protected bool bJump;
    //bool for on ground
    protected bool bGround;
    ///<summary>if player now stand on the ground</summary>
    public bool IsGround { get { return bGround; } }
    //store horizontal velocity
    protected float moveHorizontal;
    //stoer what direction is player facing true=facing right direction false=facing left direction
    protected bool bFacingRight;
    /// <summary>if player now facing at right direction
    public bool IsPlayerFacingRight { get { return bFacingRight; } }
    //set all info from props
    //set detectGround
    private void Awake ( ) {
        rb = GetComponent<Rigidbody2D> ( );
        detectGround = transform.Find ("Foot");
    }

    override protected void Update ( ) {
        base.Update ( );
        IsGrounded ( );
        //if player on ground set speed to airspeed
        moveHorizontal = Input.GetAxisRaw ("Horizontal") * (bGround?Parent.Stats.walkSpeed.Value : Parent.Stats.airSpeed.Value);
        //if player hit jump button call jump method
        if (Input.GetButtonDown ("Jump")&&IsGround) {
            Jump ( );
        }
    }

    //keep detect ground and call Move and Jump function 
    override protected void FixedUpdate ( ) {
        base.FixedUpdate ( );
        Move ( );
        InJump ( );
    }

    //if can jump set bJump to true bGround false plus numNowJump
    protected virtual void Jump ( ) {
        bJump = true;
        bGround = false;
    }

    //get horiziontal velocity and move rigidbody call in fixed update
    protected virtual void Move ( ) {
        if (moveHorizontal > 0) {
            bFacingRight = true;
            Parent.State = "WALK";
        }
        else if (moveHorizontal < 0) {
            bFacingRight = false;
            Parent.State = "WALK";
        }
        else if (moveHorizontal == 0 && Parent.State == "WALK") {
            Parent.State = "IDLE";
        }
        Vector2 targetVelocity = new Vector2 (moveHorizontal * Time.fixedDeltaTime, rb.velocity.y);
        rb.velocity = Vector2.SmoothDamp (rb.velocity, targetVelocity, ref refVelocity, smoothDamp);
    }

    //if can jump add force to rigidbody  call in fixed update
    protected virtual void InJump ( ) {
        if (bJump) {
            Parent.State = "JUMP";
            Vector2 temp = rb.velocity;
            temp.y = 0.0f;
            rb.velocity = temp;
            rb.AddForce (new Vector2 (0f, Parent.Stats.jumpForce.Value), ForceMode2D.Impulse);
            bJump = false;
        }
    }

    //keep detect if player is on ground if true set numOnojump to zero bGround = true
    protected void IsGrounded ( ) {
        if (detectGround != null) {
            foreach (LayerMask layer in groundLayer) {
                Collider2D [ ] colliders = Physics2D.OverlapPointAll (detectGround.position, layer);
                foreach (Collider2D collider in colliders) {
                    if (collider != gameObject) {
                        bGround = true;
                        if (Parent.State == "JUMP")
                            Parent.State = "IDLE";
                    }
                    else
                        bGround = false;
                }
            }
        }
    }
}
