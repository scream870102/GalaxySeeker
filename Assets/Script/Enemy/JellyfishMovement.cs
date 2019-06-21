using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>define how jellyfish move</summary>
[System.Serializable]
public class JellyfishMovement : CharacterComponent {
    // move speed default value =  1.0f
    float speed = 1f;
    // how big of ellipse from jellyfish original pos
    Vector2 range;
    // original pos of jellyfish
    Vector2 initPos = new Vector2 ( );
    /// <summary>if jellyfish reached target pos</summary>
    /// <remarks>true = keep moving false = need to find a new target pos</remarks>
    bool bDirChanged = false;
    /// <summary>target position</summary>
    Vector2 targetPos;
    // save ref for tracing target
    Transform target;
    // is player find target
    bool bFindTarget = false;
    // how big the circle which is to detect player
    float detectAreaRadius;
    // if jellyfish now facing at Right direction
    bool bFacingRight;
    // how fast when jellyfish find target and try to trace it
    float traceSpeed;

    public JellyfishMovement (Enemy parent, float speed, float traceSpeed, Vector2 range, float detectAreaRadius) : base (parent) {
        //set initPosition to jellyfish current position
        initPos = Parent.tf.position;
        this.speed = speed;
        this.range = range;
        this.detectAreaRadius = detectAreaRadius;
        this.traceSpeed = traceSpeed;
    }

    protected override void Tick ( ) {
        //keep moving jellyfish to approach target position
        if (bDirChanged) {
            if (!bFindTarget) {
                Parent.tf.position = Vector2.MoveTowards (Parent.tf.position, targetPos, speed * Time.deltaTime);
                if ((Vector2) Parent.tf.position == targetPos)
                    bDirChanged = false;
            }
            else if (bFindTarget) {
                Parent.tf.position = Vector2.Lerp (Parent.tf.position, targetPos, traceSpeed * Time.deltaTime);
                bDirChanged = false;
            }
        }
        //find new target position
        //set what direction should jellyfish face
        else if (!bDirChanged) {
            if (bFindTarget) {
                targetPos = (Vector2) (target.position - Parent.tf.position);
                targetPos = (Vector2) Parent.tf.position + targetPos.normalized * Random.Range (range.x / 2, range.x);
            }
            else {
                Vector2 dir = Random.insideUnitCircle;
                dir.Normalize ( );
                targetPos = initPos + dir * new Vector2 (Random.Range (0f, range.x), Random.Range (0f, range.y));
            }
            bDirChanged = true;
            //Change jellyfish render direction
            Vector2 temp = targetPos - (Vector2) Parent.tf.transform.position;
            bFacingRight = temp.x > 0f;
            Vector3 tmp = Parent.tf.localScale;
            tmp.x = Mathf.Abs (tmp.x) * (bFacingRight?1f: -1f);
            Parent.tf.localScale = tmp;
        }
    }

    protected override void FixedTick ( ) {
        if (!bFindTarget) {
            bFindTarget = FindTartget ( );
        }
    }

    //use circlecast to find if player in detect area
    bool FindTartget ( ) {
        RaycastHit2D hit = Physics2D.CircleCast (Parent.tf.position, detectAreaRadius, Vector2.zero, 0f, 1 << 11);
        if (hit) {
            bFindTarget = true;
            target = hit.transform;
            return true;
        }
        return false;
    }
}
