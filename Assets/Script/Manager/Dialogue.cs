using System.Collections;
using System.Collections.Generic;

using UnityEngine;

[System.Serializable]
public class Dialogue {
    /// <summary>Name which this dialogue belongs to</summary>
    [HideInInspector]
    public string name;
    /// <summary>content of dialogue</summary>
    [TextArea (3, 10)]
    public string [ ] sentences;

}
