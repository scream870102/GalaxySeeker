using UnityEngine;
/// <summary>A base class for all character </summary>
[RequireComponent (typeof (Animator))]
public abstract class AEnemy : Character {
    Player player;
    public Player Player { get { return player; } }
    public float DistanceBetweenPlayer { get { return Vector2.Distance (this.tf.position, this.Player.tf.position); } }
    Animator animator;
    public Animator Animator { get { return animator; } protected set { animator = value; } }
    public System.Action<Collider2D> OnTriggerEnter;
    public System.Action<Collider2D> OnTriggerStay;
    public System.Action<Collider2D> OnTriggerExit;
    public System.Action<Collision2D> OnColliderEnter;
    public System.Action<Collision2D> OnColliderStay;
    public System.Action<Collision2D> OnColliderExit;
    /// <summary>define what action should this monoClass do when it awaked</summary>
    protected virtual void Init ( ) {
        Stats.Init ( );
        Stats.OnHealthReachedZero += Dead;
        animator = GetComponent<Animator> ( );
        player = GameObject.FindGameObjectWithTag ("Player").GetComponent<Player> ( );
    }

    /// <summary>define action when character dead</summary>
    /// <remarks>sub class can override </remarks>
    protected virtual void Dead ( ) { }
    protected virtual void OnCollisionEnter2D (Collision2D other) {
        if (OnColliderEnter != null) OnColliderEnter (other);
    }
    protected virtual void OnCollisionStay2D (Collision2D other) {
        if (OnColliderStay != null) OnColliderStay (other);
    }
    protected virtual void OnCollisionExit2D (Collision2D other) {
        if (OnColliderExit != null) OnColliderExit (other);
    }
    protected virtual void OnTriggerEnter2D (Collider2D other) {
        if (OnTriggerEnter != null) OnTriggerEnter (other);
    }
    protected virtual void OnTriggerStay2D (Collider2D other) {
        if (OnTriggerStay != null) OnTriggerStay (other);
    }
    protected virtual void OnTriggerExit2D (Collider2D other) {
        if (OnTriggerExit != null) OnTriggerExit (other);
    }
}
