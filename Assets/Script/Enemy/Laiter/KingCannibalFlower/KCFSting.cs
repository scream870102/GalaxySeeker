using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFSting : AKingCannibalFlowerComponent {
        bool bTouched = false;
        Transform stingTf = null;
        [SerializeField] Vector2 force = Vector2.zero;
        [SerializeField] float damage = 0f;
        [SerializeField] float stingYpos = 0f;
        [SerializeField] float stingXOffset = 0f;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<KingCannibalFlower> ( );
                stingTf = Parent.ScratchASting.transform;
            }
            bTouched = false;
            Vector3 stingLaunchPos = Parent.Player.tf.position - Parent.tf.position;
            stingLaunchPos /= Parent.tf.localScale.x;
            stingLaunchPos.x += stingXOffset;
            stingLaunchPos.y = stingYpos;
            stingTf.localPosition = stingLaunchPos;

        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!bTouched) {
                int hits = Parent.ScratchAStingColTrigger ( );
                if (hits > 0) {
                    bTouched = true;
                    Parent.Player.AddRelativeForce (force);
                    Parent.Player.TakeDamage (damage);
                }
            }
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            this.Parent.ChooseNextAction ( );
        }
    }
}
