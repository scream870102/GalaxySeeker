using GalaxySeeker.Move;

using UnityEngine;
/// <summary>RedAirStingray</summary>
namespace GalaxySeeker.Enemy.RedAirStingray {
    [System.Serializable]
    public class RASMove : ARedAirStingrayComponent {
        PingPongMove horiMove = null;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<RedAirStingray> ( );
                horiMove = new PingPongMove (Parent.Props.MoveSpeed, Parent.Props.MoveRange, Parent.InitPos);
            }
            horiMove.SetVerticalPos (Parent.tf.position.y);
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            CheckTouch ( );
            Parent.tf.position = horiMove.GetNextPos (Parent.tf.position);
            Parent.UpdateRenderDirectionWithFlip (horiMove.IsFacingRight, true);

        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
        void CheckTouch ( ) {
            //if touch by player
            Parent.IsTouchedByPlayer = Parent.GetTouchPlayer ( );
            if (Parent.IsTouchedByPlayer) {
                Parent.ChooseNextAction ( );
                return;
            }
            //if touch ground should change direction??
            bool IsTouchUnderGround = false;
            ETouchType type = Parent.GetTouchGround (out IsTouchUnderGround);
            if (type == ETouchType.LEFT)
                horiMove.IsFacingRight = true;
            else if (type == ETouchType.RIGHT)
                horiMove.IsFacingRight = false;
        }
    }
}
