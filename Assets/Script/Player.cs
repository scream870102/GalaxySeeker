using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Player : Character {
    private void Awake ( ) {
        stats.OnHealthReachedZero += Dead;
    }
    // Start is called before the first frame update
    void Start ( ) {

    }

    // Update is called once per frame
    void Update ( ) {

    }

    void Dead ( ) {
        Debug.Log (stats.name + "already Dead");
    }
}
