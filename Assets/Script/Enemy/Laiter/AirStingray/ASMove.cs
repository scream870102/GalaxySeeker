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
