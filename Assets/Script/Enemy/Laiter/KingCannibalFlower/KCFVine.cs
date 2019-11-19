using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFVine : AKingCannibalFlowerComponent {
        [SerializeField] float damage = 0f;
        int hits = 0;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent)
                Parent = animator.GetComponent<KingCannibalFlower> ( );
            hits = 0;
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            hits += Parent.VineColTrigger ( );
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (hits >= Parent.Vines.GetLength(0)) hits = Parent.Vines.GetLength(0);
            for (int i = 0; i < hits; i++)
                Parent.Player.TakeDamage (damage);
            this.Parent.ChooseNextAction ( );
        }
    }
}
