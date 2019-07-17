using System.Collections.Generic;

using Eccentric.UnityUtils.Move;

using UnityEngine;
[System.Serializable]
/// <summary>define how RedAirStingray move</summary>
public class RedAirStingrayMovement : CharacterComponent {
    //-----ref
    float pingPongVelocity;
    float rangeVelocity;
    LayerMask targetLayer;
    Collider2D col;
    Vector2 moveRange;
    Vector2 initPos;
    AnimationClip rageAnim;

    //------field
    //this field is for player and will be not equal to null when player stand on it
    Transform targetTransform = null;
    //field for action keep stingray do a pingpong move
    PingPongMove pingPongMove = null;
    //field for action keep stingray do a randomRange move
    RangeRandomMove rangeMove = null;
    //field to store what the current movement does RedAirStingray use
    AMove currentMove = null;
    RedAirStingray rasg = null;

    public RedAirStingrayMovement (Enemy parent, float pingPongVelocity, float rangeVelocity, Vector2 moveRange, AnimationClip rageAnim, LayerMask targetLayer, Collider2D collider) : base (parent) {
        this.moveRange = moveRange;
        this.pingPongVelocity = pingPongVelocity;
        this.rangeVelocity = rangeVelocity;
        this.targetLayer = targetLayer;
        this.col = collider;
        this.rageAnim = rageAnim;
        initPos = this.Parent.tf.position;
        rangeMove = new RangeRandomMove (this.moveRange, this.rangeVelocity, this.initPos, false);
        pingPongMove = new PingPongMove (this.pingPongVelocity, this.moveRange.x, initPos, true, false);
        //the init move will be pingPong
        currentMove = pingPongMove;
        rasg = Parent as RedAirStingray;

    }

    protected override void FixedTick ( ) {
        GetTouch ( );
        Parent.tf.position = currentMove.GetNextPos (Parent.tf.position);
    }

    void GetTouch ( ) {
        bool bTouchPlayer = false;
        List<Collider2D> colliders = new List<Collider2D> ( );
        col.OverlapCollider (new ContactFilter2D ( ), colliders);
        foreach (Collider2D collider in colliders) {
            //if the collider is ground enter this state
            if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
                //if now use pingPongMove will change its direction due to direction of collider
                if (currentMove == pingPongMove) {
                    if (Eccentric.UnityUtils.Physics2D.IsRight (Parent.tf.position, collider.transform.position))
                        pingPongMove.IsFacingRight = false;
                    else if (Eccentric.UnityUtils.Physics2D.IsRight (Parent.tf.position, collider.transform.position))
                        pingPongMove.IsFacingRight = true;
                }
                //if now use rangeMove will try to find another target position
                else if (currentMove == rangeMove)
                    rangeMove.FindNewTargetPos ( );
            }
            //if collider with player and player stand on it change moveMode to rangeMove
            if (collider.gameObject.tag == "Player") {
                if (Eccentric.UnityUtils.Physics2D.IsAbove (Parent.tf.position, collider.transform.position)) {
                    targetTransform = collider.transform;
                    targetTransform.SetParent (Parent.tf);
                    bTouchPlayer = true;
                    currentMove = rangeMove;
                    if (!rasg.Anim.IsPlaying (rageAnim.name))
                        rasg.Anim.Play (rageAnim.name);
                }
            }
        }

        // if player out of it set parent to null
        if (!bTouchPlayer && targetTransform) {
            targetTransform.SetParent (null);
            targetTransform = null;
            pingPongMove.SetVerticalPos (Parent.tf.position.y);
            currentMove = pingPongMove;
            rasg.Anim.Play (rasg.Anim.clip.name);
        }

    }
    protected override void Disable ( ) {
        if (targetTransform) {
            targetTransform.SetParent (null);
            targetTransform = null;
        }
    }

}
