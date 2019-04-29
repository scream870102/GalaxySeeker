using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Rope : Item {
    override protected void UsingItem ( ) {
        Debug.Log ("Using " + name);
        AlreadUsed ( );
    }
    override protected void Reset ( ) {
        Debug.Log ("Reset "+ name);
    }
}
