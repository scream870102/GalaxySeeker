using GalaxySeeker.Move;

using UnityEngine;
/// <summary>RedAirStingray</summary>
namespace GalaxySeeker.Enemy.RedAirStingray {
    [System.Serializable]
    public class RASRandMove : ARedAirStingrayComponent {
        [SerializeField] float speed = 0f;
        [SerializeField] Vector2 range = Vector2.zero;
        RangeRandomMove randMove = null;
        Transform target = null;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<RedAirStingray> ( );
                randMove = new RangeRandomMove (range, speed, Parent.InitPos);
            }
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            CheckTouch ( );
            Parent.tf.position = randMove.GetNextPos (Parent.tf.position);
            Parent.UpdateRenderDirectionWithFlip (randMove.IsFacingRight);
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
        void CheckTouch ( ) {
            Parent.IsTouchedByPlayer = Parent.GetTouchPlayer ( );
            if (Parent.IsTouchedByPlayer) {
                target = Parent.Player.tf;
                target.SetParent (Parent.tf);
            }
            else if (target) {
                target.SetParent (null);
                target = null;
            }
            bool bTouchUnderGround = false;
            ETouchType type = Parent.GetTouchGround (out bTouchUnderGround);
            if (type != ETouchType.NONE)
                randMove.FindNewTargetPos ( );
        }
    }
}
