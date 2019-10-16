using UnityEngine;
namespace GalaxySeeker.Enemy.AirStingray {
    public class AirStingray : AEnemy {
        bool bTouchedPlayer = false;
        new Collider2D collider = null;
        public Collider2D Collider { get { return collider; } }
        public bool IsTouchedByPlayer { set { bTouchedPlayer = value; } }
        void Awake ( ) {
            Init ( );
            collider = GetComponent<Collider2D> ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am Airstingray I come from hell");
        }

    }
    public class AAirStingrayComponent : AEnemyComponent {
        public AirStingray Parent { get { return this.parent as AirStingray; } protected set { this.parent = value; } }
    }
}
