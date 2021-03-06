﻿using GalaxySeeker.Move;

using UnityEngine;
/// <summary>RedAirStingray</summary>
namespace GalaxySeeker.Enemy.RedAirStingray {
    [System.Serializable]
    public class RASRandMove : ARedAirStingrayComponent {
        [SerializeField] float speed = 0f;
        [SerializeField] Vector2 range = Vector2.zero;
        RangeRandomMove randMove = null;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<RedAirStingray> ( );
                randMove = new RangeRandomMove (range, speed, Parent.InitPos);
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
            bool tmp = Parent.GetTouchPlayer ( );
            if (tmp)
                Parent.Player.tf.SetParent (Parent.tf);
            else
                Parent.Player.tf.SetParent (null);
            Parent.IsTouchedByPlayer = tmp;
            ETouchType type = Parent.GetTouchGround ( );
            if (type != ETouchType.NONE)
                randMove.FindNewTargetPos ( );
        }
    }
}
