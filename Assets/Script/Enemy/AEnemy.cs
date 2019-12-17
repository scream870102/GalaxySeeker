using System.Collections.Generic;

using UnityEngine;
namespace GalaxySeeker.Enemy {
    /// <summary>A base class for all character </summary>
    [RequireComponent (typeof (Animator))]
    [RequireComponent (typeof (SpriteRenderer))]
    public abstract class AEnemy : Character {
        Player player = null;
        Animator animator = null;
        new SpriteRenderer renderer = null;
        int groundLayer = 0;
        protected bool bEnable = true;
        [SerializeField] List<Action> actions = new List<Action> ( );
        [SerializeField] LayerMask playerLayer = 0;
        public LayerMask PlayerLayer => playerLayer;
        public List<Action> Actions { get { return actions; } protected set { actions = value; } }
        public bool IsEnable { get { return bEnable; } set { bEnable = value; EnableEnemy ( ); } }
        public bool IsFacingRight { get; protected set; }
        public Player Player => player;
        public float DistanceBetweenPlayer => Vector2.Distance (this.tf.position, this.Player.tf.position);
        public Animator Animator { get { return animator; } protected set { animator = value; } }
        public SpriteRenderer Renderer { get { return renderer; } protected set { renderer = value; } }
        public int GroundLayer => groundLayer;
        public System.Action<Collider2D> OnTriggerEnter = null;
        public System.Action<Collider2D> OnTriggerStay = null;
        public System.Action<Collider2D> OnTriggerExit = null;
        public System.Action<Collision2D> OnColliderEnter = null;
        public System.Action<Collision2D> OnColliderStay = null;
        public System.Action<Collision2D> OnColliderExit = null;
        /// <summary>define what action should this monoClass do when it awaked</summary>
        protected virtual void Init ( ) {
            Stats.Init ( );
            Stats.OnHealthReachedZero += Dead;
            animator = GetComponent<Animator> ( );
            renderer = GetComponent<SpriteRenderer> ( );
            player = GameManager.Instance.Player;
            groundLayer = LayerMask.NameToLayer ("Ground");
            if (this.Actions.Count != 0) {
                foreach (Action act in this.Actions) {
                    act.InitAction ( );
                }
            }
        }

        /// <summary>define action when character dead</summary>
        /// <remarks>sub class can override </remarks>
        protected virtual void Dead ( ) { }
        protected virtual void OnCollisionEnter2D (Collision2D other) {
            if (OnColliderEnter != null)OnColliderEnter (other);
        }
        protected virtual void OnCollisionStay2D (Collision2D other) {
            if (OnColliderStay != null)OnColliderStay (other);
        }
        protected virtual void OnCollisionExit2D (Collision2D other) {
            if (OnColliderExit != null)OnColliderExit (other);
        }
        protected virtual void OnTriggerEnter2D (Collider2D other) {
            if (OnTriggerEnter != null)OnTriggerEnter (other);
        }
        protected virtual void OnTriggerStay2D (Collider2D other) {
            if (OnTriggerStay != null)OnTriggerStay (other);
        }
        protected virtual void OnTriggerExit2D (Collider2D other) {
            if (OnTriggerExit != null)OnTriggerExit (other);
        }

        /// <summary>Call this method to choose Next action from Action list which is ready CAN OVERRIDE</summary>
        /// <remarks>if action is ready and the distance between player is smaller than the setting distance will add this action to list.Finally choose the action randomly by probability</remarks>
        public virtual void ChooseNextAction ( ) {
            List<Action> ableActions = new List<Action> ( );
            float distance = Vector2.Distance (this.tf.position, this.Player.tf.position);
            Action actResult;
            //先將可以使用的行動收集起來
            foreach (Action act in this.Actions) {
                if (act.IsCanUse && act.Distance >= distance)
                    ableActions.Add (act);
            }
            //從中選出行動
            if (ableActions.Count == 0)return;
            actResult = ableActions [ActionSelector.SelectWithProbability (ableActions)];
            actResult.ResetCD ( );
            //根據行動切換對應的動畫
            if (actResult.Trigger != "")
                this.Animator.SetTrigger (actResult.Trigger);
        }

        /// <summary>Update if player is at right direction and change the facing direction due to player position by setting scale </summary>
        public void UpdateRenderDirectionWithPlayerPos (bool IsInvert = false) {
            IsFacingRight = Physics2D.IsRight (tf.position, Player.tf.position);
            Render.ChangeDirection (IsFacingRight, tf, IsInvert);
        }

        public void UpdateRenderDirection (bool IsFacingRight, bool IsInvert = false) {
            this.IsFacingRight = IsFacingRight;
            Render.ChangeDirection (this.IsFacingRight, tf, IsInvert);
        }

        public void UpdateRenderDirectionWithFlip (bool IsFacingRight, bool IsInvert = false) {
            this.IsFacingRight = IsInvert?!IsFacingRight : IsFacingRight;
            Render.ChangeDirectionXWithSpriteRender (this.IsFacingRight, renderer);
        }
        public void UpdateRenderDirectionWithPlayerPosByFlip (bool IsInvert = false) {
            IsFacingRight = Physics2D.IsRight (tf.position, Player.tf.position);
            Render.ChangeDirectionXWithSpriteRender (IsInvert?!IsFacingRight : IsFacingRight, renderer);
        }

        protected virtual void EnableEnemy ( ) {
            Animator.enabled = bEnable;
            Renderer.enabled = bEnable;
        }
    }
}
