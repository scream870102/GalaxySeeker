using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy.KingCannibalFlower {
    public class KingCannibalFlower : AEnemy {
        const int VINE_NUM = 2;
        const int NEEDLE_NUM = 2;
        readonly string stage1String = "StageOne";
        readonly string stage2String = "StageTwo";
        [SerializeField] Collider2D [] vines = new Collider2D [VINE_NUM];
        [SerializeField] Collider2D [] needles = new Collider2D [NEEDLE_NUM];
        [SerializeField] Transform [] needlesLaunchPoints = new Transform [NEEDLE_NUM];
        [SerializeField] Transform scratchLaunchPoint = null;
        [SerializeField] Collider2D scratchAsting = null;
        public Collider2D [] Vines { get => vines; }
        public Collider2D [] Needles { get => needles; }
        public Collider2D ScratchASting { get => scratchAsting; }
        public Transform [] NeedlesLaunchPoints { get => needlesLaunchPoints; }
        public Transform ScratchLaunchPoint { get => scratchLaunchPoint; }

        //public Transform[] NeedlesLaunchPoints{get=>RenderTextureCreationFlags needlesLaunchPoints;}

        void Awake ( ) {
            Init ( );
            Stats.OnHealthChanged += OnHealthChanged;
            Animator.SetBool (stage1String, true);
            Animator.SetBool (stage2String, false);
        }
        override protected void Dead ( ) {
            Animator.SetBool ("Dead", true);
            Debug.Log ("I am KingCannibalFlowerI come from hell");
        }

        void OnHealthChanged (float health) {
            Animator.SetTrigger ("TakeDamage");
            if (health <= Stats.MaxHealth / 2f)
                Animator.SetBool (stage2String, true);
        }
        void OnDestroy ( ) {
            Stats.OnHealthChanged -= OnHealthChanged;
        }
        public void UpdateRenderDirectionWithPlayerPosY (bool IsInvert = false) {
            IsFacingRight = Physics2D.IsRight (tf.position, Player.tf.position);
            Render.ChangeDirectionY (IsFacingRight, tf, IsInvert);
        }

        public int VineColTrigger ( ) {
            int count = 0;
            foreach (Collider2D col in vines) {
                List<Collider2D> cols = new List<Collider2D> ( );
                ContactFilter2D filter = new ContactFilter2D ( );
                filter.SetLayerMask (PlayerLayer);
                col.OverlapCollider (filter, cols);
                foreach (Collider2D co in cols)
                    count++;
            }
            return count;
        }

        public int NeedleColTrigger ( ) {
            int count = 0;
            foreach (Collider2D col in needles) {
                List<Collider2D> cols = new List<Collider2D> ( );
                ContactFilter2D filter = new ContactFilter2D ( );
                filter.SetLayerMask (PlayerLayer);
                col.OverlapCollider (filter, cols);
                foreach (Collider2D co in cols)
                    count++;
            }
            return count;
        }

        public int ScratchAStingColTrigger ( ) {
            int count = 0;
            List<Collider2D> cols = new List<Collider2D> ( );
            ContactFilter2D filter = new ContactFilter2D ( );
            filter.SetLayerMask (PlayerLayer);
            scratchAsting.OverlapCollider (filter, cols);
            foreach (Collider2D co in cols)
                count++;
            return count;
        }

    }
    public class AKingCannibalFlowerComponent : AEnemyComponent {
        public KingCannibalFlower Parent { get { return this.parent as KingCannibalFlower; } protected set { this.parent = value; } }
    }
}
