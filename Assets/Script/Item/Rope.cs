using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Rope : Item {
    override protected void UsingItem ( ) {
        Debug.Log ("Using Rope");
        AlreadUsed ( );
    }
    override protected void Reset ( ) {
        Debug.Log("Reset rope");
    }
}
