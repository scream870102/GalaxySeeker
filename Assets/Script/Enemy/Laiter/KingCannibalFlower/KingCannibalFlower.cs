using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    public class KingCannibalFlower : AEnemy {
        readonly string stage1String = "StageOne";
        readonly string stage2String = "StageTwo";
        [SerializeField] List<Collider2D> vines = new List<Collider2D> ( );
        [SerializeField] List<Collider2D> needles = new List<Collider2D> ( );
        public List<Collider2D> Vines { get { return vines; } }
        public List<Collider2D> Needles { get { return needles; } }

        void Awake ( ) {
            Init ( );
            Stats.OnHealthChanged += OnHealthChanged;
            Animator.SetBool (stage1String, true);
            Animator.SetBool (stage2String, false);
        }
        override protected void Dead ( ) {
            Debug.Log ("I am KingCannibalFlowerI come from hell");
        }

        void OnHealthChanged (float health) {
            Debug.Log ("Health Changed" + health);
            if (health <= Stats.MaxHealth / 2f)
                Animator.SetBool (stage2String, true);
        }
        void OnDestroy ( ) {
            Stats.OnHealthChanged -= OnHealthChanged;
        }
        public void UpdateRenderDirectionWithPlayerPosY ( ) {
            IsFacingRight = Physics2D.IsRight (tf.position, Player.tf.position);
            Render.ChangeDirectionY (IsFacingRight, tf);
        }

    }
    public class AKingCannibalFlowerComponent : AEnemyComponent {
        public KingCannibalFlower Parent { get { return this.parent as KingCannibalFlower; } protected set { this.parent = value; } }
    }
}
