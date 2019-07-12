using System.Collections.Generic;

using Eccentric.UnityUtils.Move;

using UnityEngine;
[System.Serializable]
/// <summary>define how AirStingray move</summary>
public class AirStingrayMovement : CharacterComponent {
    //-----ref
    float moveSpeed;
    float moveRange;
    LayerMask targetLayer;
    float sinkSpeed;
    Collider2D col;

    //------field
    //the center point of stingray hori movement
    Vector2 initPos;
    //field for action keep stingray do a pingpong move
    HorizontalLoopMove horiLoopMove = null;
    //this field is for player and will be not equal to null when player stand on it
    Transform targetTransform = null;

    public AirStingrayMovement (Enemy parent, float moveSpeed, float moveRange, float sinkSpeed, LayerMask targetLayer, Collider2D collider) : base (parent) {
        this.moveRange = moveRange;
        this.moveSpeed = moveSpeed;
        this.targetLayer = targetLayer;
        this.sinkSpeed = sinkSpeed;
        this.col = collider;
        initPos = this.Parent.tf.position;
        horiLoopMove = new HorizontalLoopMove (moveSpeed, moveRange, initPos, true, false);

    }

    protected override void FixedTick ( ) {
        //keep moving 
        Parent.tf.position = horiLoopMove.GetNextPos ( );
        GetTouch ( );
    }
    void GetTouch ( ) {
        //Get all collider contact with stingray
        bool bTouchPlayer = false;
        List<Collider2D> colliders = new List<Collider2D> ( );
        col.OverlapCollider (new ContactFilter2D ( ), colliders);
        foreach (Collider2D collider in colliders) {
            //if ground under stingray set verticalposition up
            //else change direction due to the ground position
            if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
                if (Eccentric.UnityUtils.Physics2D.IsUnder (Parent.tf.position, collider.transform.position))
                    horiLoopMove.SetVerticalPos (horiLoopMove.VerticalPos + sinkSpeed * Time.fixedDeltaTime);
                else if (Eccentric.UnityUtils.Physics2D.IsRight (Parent.tf.position, collider.transform.position))
                    horiLoopMove.IsFacingRight = false;
                else
                    horiLoopMove.IsFacingRight = true;
            }
            //if touch player and player is above it start to sink
            //Also set stingray transform as player parent
            if (collider.gameObject.tag == "Player") {
                if (Eccentric.UnityUtils.Physics2D.IsAbove (Parent.tf.position, collider.transform.position)) {
                    targetTransform = collider.transform;
                    targetTransform.SetParent (Parent.tf);
                    horiLoopMove.SetVerticalPos (horiLoopMove.VerticalPos - sinkSpeed * Time.fixedDeltaTime);
                    bTouchPlayer = true;
                }
            }
        }
        //if doesn't touchPlayer set targetTransform to null
        if (!bTouchPlayer && targetTransform) {
            targetTransform.SetParent (null);
            targetTransform = null;
        }
    }
    protected override void Disable ( ) {
        if (targetTransform) {
            targetTransform.SetParent (null);
            targetTransform = null;
        }
    }
}
