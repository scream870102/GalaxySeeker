using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>A base class for all character </summary>
public class Character : MonoBehaviour {
    /// <summary>character name</summary>
    new public string name;
    protected CharacterStats stats;
    public CharacterStats Stats { get { return stats; } }
}
