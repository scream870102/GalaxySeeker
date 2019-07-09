using Eccentric.UnityUtils;

using UnityEngine;
/// <summary>A base class for all character </summary>
public class Character : MonoBehaviour {
    /// <summary>character name</summary>
    new public string name;
    [SerializeField]
    /// <summary>filed of health and some method</summary>
    protected CharacterStats stats;
    /// <summary>props for character stats READONLY</summary>
    /// <remarks>include health and TakeDamage Heal method</remarks>
    public CharacterStats Stats { get { return stats; } }
    /// <summary>ref for gameObject.transform</summary>
    public Transform tf { get { return this.gameObject.transform; } }
}
