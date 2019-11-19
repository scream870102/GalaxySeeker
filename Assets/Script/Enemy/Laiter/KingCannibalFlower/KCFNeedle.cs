// TODO:
// FIXME:
using System.Collections.Generic;

using UnityEngine;
/// <summary>KingCannibalFlower</summary>
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    [System.Serializable]
    public class KCFNeedle : AKingCannibalFlowerComponent {
        const int NEEDLE_NUM = 2;
        [SerializeField] float damage = 0f;
        [SerializeField] float speed = 0f;
        List<Transform> needlesTf = new List<Transform> ( );
        Vector2 [] directions = new Vector2 [NEEDLE_NUM];
        float [] degrees = new float [NEEDLE_NUM];
        int hits = 0;
        override public void OnStateEnter (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (!Parent) {
                Parent = animator.GetComponent<KingCannibalFlower> ( );
                for (int i = 0; i < Parent.Needles.GetLength (0); i++) {
                    needlesTf.Add (Parent.Needles [i].transform);
                }
            }
            hits = 0;
            for (int i = 0; i < Parent.Needles.GetLength (0); i++) {
                needlesTf [i].localPosition = Parent.NeedlesLaunchPoints [i].localPosition;
                directions [i] = (Parent.Player.tf.position - needlesTf [i].position).normalized;
                degrees [i] = Mathf.Atan2 (directions [i].y, directions [i].x) * Mathf.Rad2Deg;
                needlesTf [i].rotation = Quaternion.Euler (0f, 0f, degrees [i] - 90f);
            }
        }
        override public void OnStateUpdate (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            hits += Parent.NeedleColTrigger ( );
            for (int i = 0; i < needlesTf.Count; i++) {
                needlesTf [i].localPosition += (Vector3)(directions [i] * speed * Time.deltaTime);
            }
        }
        override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            if (hits >= Parent.Needles.GetLength (0))hits = Parent.Needles.GetLength (0);
            for (int i = 0; i < hits; i++)
                Parent.Player.TakeDamage (damage);
            this.Parent.ChooseNextAction ( );
        }
    }
}
