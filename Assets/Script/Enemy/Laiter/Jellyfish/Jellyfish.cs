using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy.Jellyfish {
    public class Jellyfish : AEnemy {
        void Awake ( ) {
            Init ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am jellyfish I come from hell");
        }
    }
    public class AJellyFishComponent : AEnemyComponent {
        public Jellyfish Parent { get { return this.parent as Jellyfish; } protected set { this.parent = value; } }
    }
}
