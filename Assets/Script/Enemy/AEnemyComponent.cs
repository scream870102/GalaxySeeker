using UnityEngine;
namespace GalaxySeeker.Enemy {
    public abstract class AEnemyComponent : StateMachineBehaviour {
        [SerializeField] protected AEnemy parent = null;
    }
}
