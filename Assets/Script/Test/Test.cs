using System.Collections;
using System.Collections.Generic;

using Eccentric.UnityUtils;

using UnityEngine;
public class Test : MonoBehaviour {
    CountdownTimer t;
    private void Start ( ) {
        t = new CountdownTimer (5f);
    }
    // Update is called once per frame
    void Update ( ) {
        Debug.Log (t.Remain + "  " + t.IsFinished);
        if (Input.GetKey (KeyCode.Z) && t.IsFinished) {
            t.Reset ( );
        }
    }
}
