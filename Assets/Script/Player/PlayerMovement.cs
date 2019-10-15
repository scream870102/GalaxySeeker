using System.Collections.Generic;

using GalaxySeeker;

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
    float smoothDamp = .05f;
    //ref velocity for smooth damp
    Vector2 refVelocity = Vector2.zero;
    //
    //reference
    //rigidbody ref
    Rigidbody2D rb = null;
    //transform for foot
    public Transform detectGround = null;
    //ref for transform
    Transform tf = null;
    //
    //
    //field
    //
    //bool for jumping state
    bool bJump;
    //bool for on ground
    bool bGround;
    ///<summary>if player now stand on the ground</summary>
    public bool IsGround { get { return bGround; } }
    //store horizontal velocity
    float moveHorizontal;
    //store what direction is player facing true=facing right direction false=facing left direction
    [SerializeField]
    bool bFacingRight;
    /// <summary>if player now facing at right direction
    public bool IsFacingRight { get { return bFacingRight; } }
    // field if player is in swinging right now
    bool bSwing;
    /// <summary>Define player is swinging with rope right now</summary>
    public bool IsSwing { set { bSwing = value; } }
    // which position does rope hook at
    Vector2 hookPoint;
    /// <summary>make rope can tell hook point for player
    public Vector2 HookPoint { set { hookPoint = value; } }
    /// <summary>return player rigidbody2d velocity</summary>
    public Vector2 Velocity { get { return rb.velocity; } set { rb.velocity = value; } }
    //if player is flying right now
    bool bFlying;
    /// <summary>define player is flying with jetpack right now</summary>
    public bool IsFlying { set { bFlying = value; } }
    //set all info from props
    //set detectGround
    void Awake ( ) {
        rb = GetComponent<Rigidbody2D> ( );
        detectGround = transform.Find ("Foot");
        bSwing = false;
        tf = gameObject.transform;
    }
    protected override void Tick ( ) {
        IsGrounded ( );
        //if player on ground set speed to airspeed
        moveHorizontal = Input.GetAxisRaw ("Horizontal") * (bGround?Parent.Props.WalkSpeed : Parent.Props.AirSpeed);
        //if player hit jump button call jump method
        if (Input.GetButtonDown ("Jump") && IsGround)
            Jump ( );
    }
    //keep detect ground and call Move and Jump function 
    protected override void FixedTick ( ) {
        Move ( );
        InJump ( );
        Render.ChangeDirection (bFacingRight, Parent.tf);
    }

    //if can jump set bJump to true bGround false plus numNowJump
    void Jump ( ) {
        bJump = true;
        bGround = false;
    }

    //get horizontal velocity and move rigidbody call in fixed update
    void Move ( ) {
        //when player is swinging change its action mode
        if (bSwing) {
            if (moveHorizontal == 0f)
                return;
            bFacingRight = moveHorizontal > 0f;
            Vector2 playerNormalVector = (hookPoint - (Vector2) transform.position).normalized;
            Vector2 swingDir = IsFacingRight?new Vector2 (playerNormalVector.y, -playerNormalVector.x) : new Vector2 (-playerNormalVector.y, playerNormalVector.x);
            rb.AddForce (swingDir * Parent.Props.SwingForce * Time.deltaTime, ForceMode2D.Force);
        }
        //this section is active when player is using Jetpack to fly
        else if (bFlying && !bGround) {
            bFacingRight = moveHorizontal > 0f;
            rb.velocity = new Vector2 ( );
            rb.AddForce (new Vector2 (moveHorizontal, Parent.Props.FlyingGasForce) * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        //this section is normal move mode about player
        else {
            if (moveHorizontal == 0f)
                Parent.State = "IDLE";
            else {
                bFacingRight = moveHorizontal > 0f;
                Parent.State = "WALK";
            }
            // if player stays on ground move it by modify rigidbody velocity
            if (bGround) {
                Vector2 targetVelocity = new Vector2 (moveHorizontal * Time.fixedDeltaTime, rb.velocity.y);
                rb.velocity = Vector2.SmoothDamp (rb.velocity, targetVelocity, ref refVelocity, smoothDamp);
            }
            // when player stay in the air move it by add force
            else if (!bGround) {
                Vector2 force = new Vector2 (moveHorizontal * Time.fixedDeltaTime, 0f);
                rb.AddForce (force, ForceMode2D.Force);
            }

        }

    }

    //if can jump add force to rigidbody  call in fixed update
    void InJump ( ) {
        if (bJump) {
            Parent.State = "JUMP";
            Vector2 temp = rb.velocity;
            temp.y = 0.0f;
            rb.velocity = temp;
            rb.AddForce (new Vector2 (0f, Parent.Props.JumpForce), ForceMode2D.Impulse);
            bJump = false;
        }
    }

    //keep detect if player is on ground if true set numOnojump to zero bGround = true
    void IsGrounded ( ) {
        if (detectGround != null) {
            foreach (LayerMask layer in groundLayer) {
                Collider2D [ ] colliders = UnityEngine.Physics2D.OverlapPointAll (detectGround.position, layer);
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

    /// <summary>public method make other can add force to player's rigidbody2d</summary>
    public void AddForce (Vector2 force, ForceMode2D mode = ForceMode2D.Impulse) {
        rb.AddForce (force, mode);
    }
}
