using GalaxySeeker.Move;

using UnityEngine;
/// <summary>RedAirStingray</summary>
namespace GalaxySeeker.Enemy.RedAirStingray {
    [System.Serializable]
    public class RASRandMove : ARedAirStingrayComponent {
        RangeRandomMove randMove = null;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<RedAirStingray> ( );
                randMove = new RangeRandomMove (Parent.Props.RandRange, Parent.Props.RandSpeed, Parent.InitPos);
            }
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            CheckTouch ( );
            Parent.tf.position = randMove.GetNextPos (Parent.tf.position);
            Parent.UpdateRenderDirectionWithFlip (randMove.IsFacingRight, true);
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
        void CheckTouch ( ) {
            bool bTouchGroundUnder=false;
            Parent.GetTouchGround(out bTouchGroundUnder);
            if(bTouchGroundUnder){
                Parent.Player.tf.SetParent(null);
                Parent.Stats.TakeDamage(Parent.Stats.CurrentHealth);
                return;
            }
            bool tmp = Parent.GetTouchPlayer ( );
            if (tmp)
                Parent.Player.tf.SetParent (Parent.tf);
            else
                Parent.Player.tf.SetParent (null);
            Parent.IsTouchedByPlayer = tmp;
            if (Parent.GetTouchGround ( ))
                randMove.FindNewTargetPos ( );
        }
    }
}
