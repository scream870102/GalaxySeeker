﻿using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy.CannibalFlower {
    public class CannibalFlower : AEnemy {
        [SerializeField] List<Collider2D> needles = new List<Collider2D> ( );
        public List<Collider2D> Needles { get { return needles; } }
        void Awake ( ) {
            Init ( );
        }
        override protected void Dead ( ) {
            Debug.Log ("I am CannibalFlower I come from hell");
        }

    }
    public class ACannibalFlowerComponent : AEnemyComponent {
        public CannibalFlower Parent { get { return this.parent as CannibalFlower; } protected set { this.parent = value; } }
    }
}
