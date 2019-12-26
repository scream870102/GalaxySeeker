using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy.Jellyfish {
    public class Jellyfish : AEnemy {
        [SerializeField] JellyfishProps props;
        public JellyfishProps Props => props;
        void Awake ( ) {
            Init ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am jellyfish I come from hell");
            this.gameObject.SetActive(false);
        }
    }

    [System.Serializable]
    public class JellyfishProps {
        [Header ("Move")]
        public float TraceDist = 0f;
        public float TraceVel = 0f;
        public float NormalVel = 0f;
        public Vector2 Range = Vector2.zero;

        [Header ("Attack")]
        public float AttackPoint = 0f;
    }
    public class AJellyFishComponent : AEnemyComponent {
        public Jellyfish Parent { get { return this.parent as Jellyfish; } protected set { this.parent = value; } }
    }
}
