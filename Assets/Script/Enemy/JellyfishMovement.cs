using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>define how jellyfish move</summary>
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
    Vector2 target;
    // if jellyfish now facing at Right
    bool bFacingRight;

    public JellyfishMovement (Enemy parent, float speed, Vector2 range) : base (parent) {
        initPos = Parent.tf.position;
        this.speed = speed;
        this.range = range;
    }

    protected override void Tick ( ) {
        //keep moving jellyfish to approach target position
        if (bDirChanged) {
            float step = speed * Time.deltaTime;
            Parent.tf.position = Vector2.MoveTowards (Parent.tf.position, target, step);
            if ((Vector2) Parent.tf.position == target)
                bDirChanged = false;
        }
        //find new target position
        //set what direction should jellyfish face
        else if (!bDirChanged) {
            Vector2 dir = Random.insideUnitCircle;
            dir.Normalize ( );
            bDirChanged = true;
            target = initPos + dir * new Vector2 (Random.Range (0f, range.x), Random.Range (0f, range.y));
            Vector2 temp = target - (Vector2) Parent.tf.transform.position;
            bFacingRight = temp.x > 0f;
            Vector3 tmp = Parent.tf.localScale;
            tmp.x = Mathf.Abs (tmp.x) * (bFacingRight?1f: -1f);
            Parent.tf.localScale = tmp;
        }
    }
}
