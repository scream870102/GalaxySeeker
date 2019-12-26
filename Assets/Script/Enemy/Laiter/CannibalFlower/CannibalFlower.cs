using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy.CannibalFlower {
    public class CannibalFlower : AEnemy {
        [SerializeField] List<Collider2D> needles = new List<Collider2D> ( );
        [SerializeField] CannibalFlowerProps props;
        public CannibalFlowerProps Props => props;
        public List<Collider2D> Needles => needles;
        void Awake ( ) {
            Init ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am CannibalFlower I come from hell");
        }

    }

    [System.Serializable]
    public class CannibalFlowerProps {
        [Header ("Bite")]
        public float BiteAtkPoint = 0f;
        [Header ("Needle")]
        public float NeedleAtkPoint = 0f;
    }

    public class ACannibalFlowerComponent : AEnemyComponent {
        public CannibalFlower Parent { get { return this.parent as CannibalFlower; } protected set { this.parent = value; } }
    }

}
