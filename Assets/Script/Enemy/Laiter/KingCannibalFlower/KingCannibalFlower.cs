using UnityEngine;
namespace GalaxySeeker.Enemy.KingCannibalFlower{
    public class KingCannibalFlower: AEnemy {
        void Awake ( ) {
            Init ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am KingCannibalFlowerI come from hell");
        }

    }
    public class AKingCannibalFlowerComponent : AEnemyComponent {
        public KingCannibalFlower Parent { get { return this.parent as KingCannibalFlower; } protected set { this.parent = value; } }
    }
}
