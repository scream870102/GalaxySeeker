using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Enemy : Character {
    [SerializeField]
    EnemyStats stats;
    public EnemyStats Stats { get { return stats; } }
    void Awake ( ) {
        Stats.OnHealthReachedZero += Dead;
        Stats.Init ( );
    }
    // Start is called before the first frame update
    void Start ( ) {

    }

    // Update is called once per frame
    void Update ( ) {

    }
    void Dead ( ) {
        Debug.Log (name + "is dead");
    }

}
