using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFNeedle : AKingCannibalFlowerComponent {
        [SerializeField] float damage;
        int hits = 0;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<KingCannibalFlower> ( );
            hits = 0;
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            hits += Parent.NeedleColTrigger ( );
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (hits >= Parent.Needles.Count) hits = Parent.Needles.Count;
            for (int i = 0; i < hits; i++)
                Parent.Player.TakeDamage (damage);
            this.Parent.ChooseNextAction ( );
        }
    }
}
