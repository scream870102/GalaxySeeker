using UnityEngine;
using Eccentric.UnityUtils.Physics2D;
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

    //---------field
    // original pos of jellyfish
    Vector2 initPos = new Vector2 ( );
    /// <summary>target position</summary>
    Vector2 targetPos;
    // save ref for tracing target
    Transform target;
    // is jellyfish need to change its direction or not
    bool bDirChanged = false;
    // is player find target
    bool bFindTarget = false;
    // if jellyfish now facing at Right direction
    bool bFacingRight;

    public JellyfishMovement (Enemy parent, float speed, float traceSpeed, Vector2 range, float detectAreaRadius, LayerMask targetLayer) : base (parent) {
        //set initPosition to jellyfish current position
        initPos = Parent.tf.position;
        this.speed = speed;
        this.range = range;
        this.detectAreaRadius = detectAreaRadius;
        this.traceSpeed = traceSpeed;
        this.targetLayer = targetLayer;
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
            target = FindTarget.CircleCast (Parent.tf.position, detectAreaRadius, targetLayer, target);
            bFindTarget = (target? true : false);
        }
    }
}
