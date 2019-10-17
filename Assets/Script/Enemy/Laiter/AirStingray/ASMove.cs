// using System.Collections.Generic;

// using GalaxySeeker;
// using GalaxySeeker.Move;

// using UnityEngine;
// /// <summary>AirStingray</summary>
// namespace GalaxySeeker.Enemy.AirStingray {
//     /// <summary>Normal move of airStingray</summary>
//     [System.Serializable]
//     public class ASMove : AAirStingrayComponent {
//         PingPongMove horiLoopMove = null;
//         [SerializeField] float moveSpeed = 0f;
//         [SerializeField] float moveRange = 0f;
//         Transform targetTransform = null;
//         override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//             if (!Parent) {
//                 Parent = animator.GetComponent<AirStingray> ( );
//                 horiLoopMove = new PingPongMove (moveSpeed, moveRange, Parent.InitPos);
//             }
//             horiLoopMove.SetVerticalPos (Parent.tf.position.y);
//         }
//         override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//             Parent.tf.position = horiLoopMove.GetNextPos (Parent.tf.position);
//             Parent.IsTouchedByPlayer = GetTouch ( );
//             Parent.UpdateRenderDirection (horiLoopMove.IsFacingRight);
//         }
//         override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
//             this.Parent.ChooseNextAction ( );
//         }

//         bool GetTouch ( ) {
//             bool bTouchPlayer = false;
//             List<Collider2D> colliders = new List<Collider2D> ( );
//             Parent.Collider.OverlapCollider (new ContactFilter2D ( ), colliders);
//             foreach (Collider2D collider in colliders) {
//                 Debug.Log (collider.gameObject.tag);
//                 if (collider.gameObject.tag == "Player" && Physics2D.IsAbove (Parent.tf.position, collider.transform.position)) {
//                     targetTransform = collider.transform;
//                     targetTransform.SetParent (Parent.tf);
//                     bTouchPlayer = true;
//                 }
//                 else if (collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
//                     if (GalaxySeeker.Physics2D.IsRight (Parent.tf.position, collider.transform.position))
//                         horiLoopMove.IsFacingRight = false;
//                     else
//                         horiLoopMove.IsFacingRight = true;
//                 }
//             }
//             if (!bTouchPlayer && targetTransform) {
//                 targetTransform.SetParent (null);
//                 targetTransform = null;
//             }
//             return bTouchPlayer;
//         }
//     }
// }
using System.Collections.Generic;

using GalaxySeeker.Move;

using UnityEngine;
/// <summary>AirStingray</summary>
namespace GalaxySeeker.Enemy.AirStingray {
    [System.Serializable]
    public class ASMove : AAirStingrayComponent {
        [SerializeField] float moveSpeed = 0f;
        [SerializeField] float moveRange = 0f;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<AirStingray> ( );
                Parent.HoriMove = new PingPongMove (moveSpeed, moveRange, Parent.InitPos);
            }
            Parent.HoriMove.SetVerticalPos (Parent.tf.position.y);
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            CheckTouch ( );
            Parent.tf.position = Parent.HoriMove.GetNextPos (Parent.tf.position);
            Parent.UpdateRenderDirectionWithFlip (Parent.HoriMove.IsFacingRight,true);
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }

        void CheckTouch ( ) {
            //if touch by player
            Parent.IsTouchedByPlayer = Parent.GetTouchPlayer ( );
            //if touch ground should change direction??
            bool IsTouchUnderGround = false;
            ETouchType type = Parent.GetTouchGround (out IsTouchUnderGround);
            if (type == ETouchType.LEFT)
                Parent.HoriMove.IsFacingRight = true;
            else if (type == ETouchType.RIGHT)
                Parent.HoriMove.IsFacingRight = false;

        }
    }
}
