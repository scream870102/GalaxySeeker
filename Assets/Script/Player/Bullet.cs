using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[RequireComponent (typeof (Collider2D), typeof (Rigidbody2D))]
public class Bullet : ObjectPoolItem {
    /// <summary>how many force will add to bullet when it is being fired</summary>
    public Vector2 force;
    //ref for rigidbody
    Rigidbody2D rb;
    //ref for transform
    Transform tf;

    void Awake ( ) {
        rb = GetComponent<Rigidbody2D> ( );
        tf = this.transform;
    }

    //other class will call this when firing the bullet
    public void Fire (Vector2 direction) {
        rb.velocity = new Vector2 ( );
        rb.AddForce (force * direction);
    }

    //init its position
    //and bullets will disappear after 5 seconds
    public override void Init ( ) {
        tf.localPosition = new Vector2 ( );
        CancelInvoke ( );
        Invoke ("Recycle", 5f);
    }

    //when enter other collider recycle self
    //exclude layers player bullets
    void OnTriggerEnter2D (Collider2D other) {
        Recycle ( );
    }
}
