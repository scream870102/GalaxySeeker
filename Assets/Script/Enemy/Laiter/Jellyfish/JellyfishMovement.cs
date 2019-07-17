using Eccentric.UnityUtils;
using Eccentric.UnityUtils.Move;

using UnityEngine;
/// <summary>define how jellyfish move</summary>
[System.Serializable]
public class JellyfishMovement : CharacterComponent {
    //-----------ref
    // move speed default value =  1.0f
    float speed = 1f;
    // how big of ellipse from jellyfish original pos
    Vector2 range;
    // how fast when jellyfish find target and try to trace it
    float traceSpeed;
    // Which layer to detect
    LayerMask targetLayer;
    // how big the circle which is to detect player
    float detectAreaRadius;
    float minDistance;

    //---------field
    // original pos of jellyfish
    Vector2 initPos = new Vector2 ( );
    /// <summary>target position</summary>
    Vector2 targetPos;
    // save ref for tracing target
    Transform target = null;
    // is player find target
    bool bFindTarget = false;
    RangeRandomMove rangeMove = null;
    // if got the target will use traceMode
    FreeTraceMove traceMove = null;
    AMove currentMove = null;

    public JellyfishMovement (Enemy parent, float speed, float traceSpeed, Vector2 range, float detectAreaRadius, float minDistance, LayerMask targetLayer) : base (parent) {
        //set initPosition to jellyfish current position
        initPos = Parent.tf.position;
        this.speed = speed;
        this.range = range;
        this.detectAreaRadius = detectAreaRadius;
        this.traceSpeed = traceSpeed;
        this.targetLayer = targetLayer;
        this.minDistance = minDistance;
        rangeMove = new RangeRandomMove (range, speed, initPos);
        traceMove = new FreeTraceMove (traceSpeed, target, Parent.tf.position, minDistance);
        currentMove = rangeMove;
    }

    protected override void Tick ( ) {
        //if find the target try to approach the target 
        if (bFindTarget) {
            currentMove = traceMove;
            if (traceMove.Target != target)
                traceMove.SetTarget (target);
            Parent.tf.position = traceMove.GetNextPos (Parent.tf.position);
        }
        //if not find the target just do RandomRangeMove
        else {
            currentMove = rangeMove;
            targetPos = rangeMove.GetNextPos (Parent.tf.position);
            Parent.tf.position = targetPos;
        }
        //Change jellyfish render direction
        Render.ChangeDirection (currentMove.IsFacingRight, Parent.tf);

    }

    protected override void FixedTick ( ) {
        if (!bFindTarget) {
            bFindTarget = Eccentric.UnityUtils.Physics2D.OverlapCircle (Parent.tf.position, detectAreaRadius, targetLayer, ref target);
        }
    }
}
