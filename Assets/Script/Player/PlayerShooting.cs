using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class PlayerShooting : PlayerComponent {
    //use object pool to manage bullet
    [SerializeField]
    ObjectPool Bullets;
    /// <summary>the time between two shooting action</summary>
    public float coolDown;
    //timer for store next shooting time
    float timer;
    //if player can shoot right now
    bool bShootable;
    //spawn all bullets
    void Awake ( ) {
        Bullets.Init ( );
        bShootable = true;
        timer = Time.time;
    }

    //if player hit shoot button shoot bullet
    protected override void Tick ( ) {
        if (Input.GetButtonDown ("Shoot") && bShootable) {
            Bullet bullet = Bullets.GetPooledObject ( ) as Bullet;
            bullet.Fire (Parent.IsFacingRight?Vector2.right : Vector2.left);
            timer = Time.time + coolDown;
            bShootable = false;
        }
        bShootable = Time.time > timer? true : false;
    }
}
