using Eccentric.Collections;

using GalaxySeeker.Enemy;

using UnityEngine;
[RequireComponent (typeof (Collider2D), typeof (Rigidbody2D))]
public class Bullet : MonoBehaviour, IObjectPoolAble {
    [SerializeField] Vector2 force;
    Rigidbody2D rb;
    Transform tf;
    //ParticleSystem ptc;
    //ref for particle system shapemodule
    //this is for change the particle emitter scale
    //ParticleSystem.ShapeModule ptcShape;
    /// <summary>which pool is this bullet belongs to</summary>
    public ObjectPool Pool { get; set; }
    float damage;

    void Awake ( ) {
        rb = GetComponent<Rigidbody2D> ( );
        tf = this.transform;
        //ptc = GetComponent<ParticleSystem> ( );
        //ptcShape = ptc.shape;
        //ptc.Pause ( );
    }

    //other class will call this when firing the bullet
    //add force to bullet and play the particle
    public void Fire (Vector2 direction, Vector3 position, float damage) {
        this.damage = damage;
        //ptcShape.scale = new Vector3 (1f, direction == Vector2.right?1f: -1f, 1f);
        tf.position = position;
        rb.velocity = new Vector2 ( );
        rb.AddForce (force * direction);
        //ptc.Play ( );

    }

    //init its position
    //and bullets will disappear after 5 seconds
    public void Init<T> (T data) {
        tf.localPosition = new Vector2 ( );
        //ptc.Pause ( );
        CancelInvoke ( );
        Invoke ("Recycle", 5f);
    }

    public void Recycle ( ) {
        Pool.RecycleObject (this);
    }

    //when enter other collider recycle self
    //exclude layers player bullets
    void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Enemy") {
            AEnemy enemy = other.gameObject.GetComponent<AEnemy> ( );
            enemy.TakeDamage (damage);
        }
        Recycle ( );
    }
}
