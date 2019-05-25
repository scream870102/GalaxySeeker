using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerShooting : PlayerComponent {
    //use object pool to manage bullet
    [SerializeField]
    ObjectPool Bullets;
    //spawn all bullets
    void Awake ( ) {
        Bullets.Init ( );
    }

    //if player hit shoot button shoot bullet
    protected override void Tick ( ) {
        if (Input.GetButtonDown ("Shoot")) {
            Bullet bullet = Bullets.GetPooledObject ( ) as Bullet;
            bullet.Fire (Parent.IsFacingRight?Vector2.right : Vector2.left);
        }
    }
}
