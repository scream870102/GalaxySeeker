using System.Collections.Generic;

using GalaxySeeker.Move;

using UnityEngine;
/// <summary>AirStingray</summary>
namespace GalaxySeeker.Enemy.AirStingray {
    [System.Serializable]
    public class ASSinkMove : AAirStingrayComponent {
        [SerializeField] float sinkSpeed = 0f;
        Transform target = null;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<AirStingray> ( );
            }
            Parent.HoriMove.SetVerticalPos (Parent.tf.position.y);
            Parent.Player.Velocity = Vector2.zero;
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            Parent.HoriMove.SetVerticalPos (Parent.tf.position.y - sinkSpeed * Time.deltaTime);
            CheckTouch ( );
            Parent.tf.position = Parent.HoriMove.GetNextPos (Parent.tf.position);
            Parent.UpdateRenderDirectionWithFlip (Parent.HoriMove.IsFacingRight, true);
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }

        void CheckTouch ( ) {
            //if touch by player
            Parent.IsTouchedByPlayer = Parent.GetTouchPlayer ( );
            if (Parent.IsTouchedByPlayer) {
                target = Parent.Player.tf;
                target.SetParent (Parent.tf);
            }
            else if (target) {
                target.SetParent (null);
                target = null;
            }
            //if touch ground should change direction??
            bool IsTouchUnderGround = false;
            ETouchType type = Parent.GetTouchGround (out IsTouchUnderGround);
            if (type == ETouchType.LEFT)
                Parent.HoriMove.IsFacingRight = true;
            else if (type == ETouchType.RIGHT)
                Parent.HoriMove.IsFacingRight = false;
            if (IsTouchUnderGround)
                Parent.HoriMove.SetVerticalPos (Parent.HoriMove.VerticalPos + sinkSpeed * Time.deltaTime);
        }
    }
}
