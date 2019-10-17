using UnityEngine;
namespace GalaxySeeker.Enemy.RedAirStingray{
    public class RedAirStingray: AEnemy {
        void Awake ( ) {
            Init ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am RedAirStingrayI come from hell");
        }

    }
    public class ARedAirStingrayComponent : AEnemyComponent {
        public RedAirStingray Parent { get { return this.parent as RedAirStingray; } protected set { this.parent = value; } }
    }
}
