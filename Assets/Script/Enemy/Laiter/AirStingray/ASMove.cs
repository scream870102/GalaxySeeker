using System.Collections.Generic;

using GalaxySeeker.Move;

using UnityEngine;
/// <summary>AirStingray</summary>
namespace GalaxySeeker.Enemy.AirStingray {
    /// <summary>Normal move of airStingray</summary>
    [System.Serializable]
    public class ASMove : AAirStingrayComponent {
        PingPongMove horiLoopMove = null;
        float moveSpeed = 0f;
        float moveRange = 0f;
        float sinkSpeed = 0f;
        Vector2 initPos = Vector2.zero;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<AirStingray> ( );
                initPos = this.Parent.tf.position;
                horiLoopMove = new PingPongMove (moveSpeed, moveRange, initPos);
            }
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Parent.tf.position = horiLoopMove.GetNextPos (Parent.tf.position);
            Parent.IsTouchedByPlayer = GetTouch ( );
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }

        bool GetTouch ( ) {
            bool bTouchPlayer = false;
            List<Collider2D> colliders = new List<Collider2D> ( );
            Parent.Collider.OverlapCollider (new ContactFilter2D ( ), colliders);
            foreach (Collider2D collider in colliders) {
                if (collider.gameObject.tag == "Player")
                    bTouchPlayer = true;
                else if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
                    if (GalaxySeeker.Physics2D.IsUnder (Parent.tf.position, collider.transform.position))
                        horiLoopMove.SetVerticalPos (horiLoopMove.VerticalPos + sinkSpeed * Time.deltaTime);
                    if (GalaxySeeker.Physics2D.IsRight (Parent.tf.position, collider.transform.position))
                        horiLoopMove.IsFacingRight = false;
                    else
                        horiLoopMove.IsFacingRight = true;
                }
            }
            return bTouchPlayer;
            // //Get all collider contact with stingray
            // List<Collider2D> colliders = new List<Collider2D> ( );
            // col.OverlapCollider (new ContactFilter2D ( ), colliders);
            // foreach (Collider2D collider in colliders) {
            //     //if ground under stingray set verticalposition up
            //     //else change direction due to the ground position
            //     if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
            //         if (GalaxySeeker.Physics2D.IsUnder (Parent.tf.position, collider.transform.position))
            //             horiLoopMove.SetVerticalPos (horiLoopMove.VerticalPos + sinkSpeed * Time.deltaTime);
            //         else if (GalaxySeeker.Physics2D.IsRight (Parent.tf.position, collider.transform.position))
            //             horiLoopMove.IsFacingRight = false;
            //         else
            //             horiLoopMove.IsFacingRight = true;
            //     }
            //     //if touch player and player is above it start to sink
            //     //Also set stingray transform as player parent
            //     else if (collider.gameObject.tag == "Player") {
            //         if (GalaxySeeker.Physics2D.IsAbove (Parent.tf.position, collider.transform.position)) {
            //             targetTransform = collider.transform;
            //             targetTransform.SetParent (Parent.tf);
            //             horiLoopMove.SetVerticalPos (horiLoopMove.VerticalPos - sinkSpeed * Time.fixedDeltaTime);
            //             bTouchPlayer = true;
            //         }
            //     }
            // }
        }
    }
}
