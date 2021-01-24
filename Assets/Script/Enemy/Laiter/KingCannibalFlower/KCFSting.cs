using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFSting : AKingCannibalFlowerComponent {
        bool bTouched = false;
        bool bAttack = false;
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
            bAttack = false;
            Vector3 stingLaunchPos = Parent.Player.tf.position - Parent.tf.position;
            stingLaunchPos /= Parent.tf.localScale.x;
            stingLaunchPos.x += stingXOffset;
            stingLaunchPos.y = stingYpos;
            stingTf.localPosition = stingLaunchPos;

        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!bTouched && Parent.ScratchASting.enabled) {
                int hits = Parent.ScratchAStingColTrigger ( );
                if (hits > 0) {
                    bTouched = true;
                    bAttack = true;
                    Parent.Player.AddRelativeForce (force);
                }
            }
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (bAttack) {
                Parent.Player.TakeDamage (damage);
            }
            this.Parent.ChooseNextAction ( );
        }
    }
}
