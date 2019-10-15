using UnityEngine;
/// <summary>A base class for all character </summary>
public class Character : MonoBehaviour {
    /// <summary>character name</summary>
    new public string name;
    /// <summary>filed of health and some method</summary>
    [SerializeField] CharacterStats stats;
    /// <summary>props for character stats READONLY</summary>
    /// <remarks>include health and TakeDamage Heal method</remarks>
    public CharacterStats Stats { get { return stats; } }
    /// <summary>ref for gameObject.transform</summary>
    public Transform tf { get { return this.gameObject.transform; } }

    /// <summary>Call this method to cause damage to player</summary>
    /// <param name="damage">how many damage will cause to player</param>
    public void TakeDamage (float damage) {
        this.Stats.TakeDamage (damage);
    }

    /// <summary>Call this method to heal the player</summary>
    /// <param name="amount">the health point to add to player</param>
    public void Heal (float amount) {
        this.Stats.Heal (amount);
    }
}
