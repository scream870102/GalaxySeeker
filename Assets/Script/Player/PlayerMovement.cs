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
    [SerializeField] float forwardAirNormalX = 0.7f;
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
    Rigidbody2D rb = null;
    //transform for foot
    Transform detectGround = null;
    //
    //
    //field
    //
    bool bJump = false;
    bool bGround = false;
    bool bFacingRight = false;
    bool bSwing = false;
    bool bFlying = false;
    bool bStun = false;
    bool bCrouched = false;
    //store horizontal velocity
    float moveHorizontal = 0f;
    // which position does rope hook at
    Vector2 hookPoint = Vector2.zero;

    ///<summary>if player now stand on the ground</summary>
    public bool IsGround => bGround;
    /// <summary>if player now facing at right direction
    public bool IsFacingRight => bFacingRight;
    /// <summary>Define player is swinging with rope right now</summary>
    public bool IsSwing { set => bSwing = value; }
    /// <summary>define player is flying with jetpack right now</summary>
    public bool IsFlying { set => bFlying = value; }
    /// <summary>define player is stun or not if true player can't move and will keep at the same position by setting velocity to zero</summary>
    // public bool IsStun { get { return bStun; } set { bStun = value; } }
    public bool IsStun { get => bStun; set => bStun = value; }
    /// <summary>define if player is crouching</summary>
    public bool IsCrouched => bCrouched;
    /// <summary>make rope can tell hook point for player
    public Vector2 HookPoint { set => hookPoint = value; }

    //set all info from props
    //set detectGround
    void Awake ( ) {
        detectGround = transform.Find ("Foot");
    }
    void Start ( ) {
        rb = Parent.Rb;
    }

    protected override void Tick ( ) {
        IsGrounded ( );
        //Crouching state
        if (Input.GetButton ("Vertical"))
            bCrouched = Input.GetAxisRaw ("Vertical") < 0.0f?true : false;
        //if player on ground set speed to airspeed
        moveHorizontal = Input.GetAxisRaw ("Horizontal") * (bGround?Parent.Props.WalkSpeed : Parent.Props.AirSpeed);
        //if player hit jump button call jump method
        if (Input.GetButtonDown ("Jump") && bGround)
            Jump ( );
    }
    //keep detect ground and call Move and Jump function 
    protected override void FixedTick ( ) {
        if (bStun) {
            rb.velocity = Vector2.zero;
            return;
        }
        Move ( );
        InJump ( );
        Render.ChangeDirection (bFacingRight, Parent.tf);
    }

    //if can jump set bJump to true bGround false plus numNowJump
    void Jump ( ) {
        bJump = true;
    }

    //get horizontal velocity and move rigidbody call in fixed update
    void Move ( ) {
        Debug.Log(moveHorizontal);
        if (IsTouchGroundForward ( ))
            moveHorizontal = 0f;
        if (moveHorizontal != 0f)
            bFacingRight = moveHorizontal > 0f;
        //when player is swinging change its action mode
        if (bSwing) {
            if (moveHorizontal == 0f)
                return;
            Vector2 playerNormalVector = (hookPoint - (Vector2)transform.position).normalized;
            Vector2 swingDir = IsFacingRight?new Vector2 (playerNormalVector.y, -playerNormalVector.x) : new Vector2 (-playerNormalVector.y, playerNormalVector.x);
            rb.AddForce (swingDir * Parent.Props.SwingForce * Time.deltaTime, ForceMode2D.Force);
        }
        //this section is active when player is using Jetpack to fly
        else if (bFlying && !bGround) {
            rb.velocity = new Vector2 ( );
            rb.AddForce (new Vector2 (moveHorizontal, Parent.Props.FlyingGasForce) * Time.fixedDeltaTime, ForceMode2D.Impulse);
        }
        //this section is normal move mode about player
        else {
            // if player stays on ground move it by modify rigidbody velocity
            if (bGround) {
                Vector2 targetVelocity = new Vector2 (moveHorizontal * Time.fixedDeltaTime, rb.velocity.y);
                rb.velocity = Vector2.SmoothDamp (rb.velocity, targetVelocity, ref refVelocity, smoothDamp);
            }
            // when player stay in the air move it by add force
            else if (!bGround) {
                Vector2 targetVelocity = new Vector2 (moveHorizontal * Time.fixedDeltaTime, rb.velocity.y);
                rb.velocity = Vector2.SmoothDamp (rb.velocity, targetVelocity, ref refVelocity, smoothDamp);
            }

        }
        Parent.Anim.SetFloat ("VelocityX", Mathf.Abs (moveHorizontal));
        Parent.Anim.SetFloat ("VelocityY", rb.velocity.y);

    }

    bool IsTouchGroundForward ( ) {
        if (!IsGround) {
            List<ContactPoint2D> point2Ds = new List<ContactPoint2D> ( );
            Parent.Col.GetContacts (point2Ds);
            foreach (ContactPoint2D item in point2Ds) {
                if (Mathf.Abs (item.normal.x) >= forwardAirNormalX)
                    return true;
            }
        }
        return false;
    }

    //if can jump add force to rigidbody  call in fixed update
    void InJump ( ) {
        if (bJump && bGround) {
            bGround = false;
            Vector2 temp = rb.velocity;
            temp.y = 0.0f;
            rb.velocity = temp;
            rb.AddForce (new Vector2 (0f, Parent.Props.JumpForce), ForceMode2D.Impulse);
            Parent.Anim.SetBool ("Jump", true);
            bJump = false;
        }
    }

    //keep detect if player is on ground if true set numOnojump to zero bGround = true
    void IsGrounded ( ) {
        if (detectGround != null) {
            foreach (LayerMask layer in groundLayer) {
                Collider2D [] colliders = UnityEngine.Physics2D.OverlapPointAll (detectGround.position, layer);
                foreach (Collider2D collider in colliders) {
                    if (collider != gameObject) {
                        bGround=true;
                        Parent.Anim.SetBool ("Jump", false);
                        return;
                    }
                    else
                        bGround = false;
                }
            }
        }
    }
}
