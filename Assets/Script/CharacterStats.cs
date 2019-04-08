using System.Collections;
using System.Collections.Generic;

using UnityEngine;
[CreateAssetMenu(fileName = "New CharacterStats", menuName = "Character/CharacterStats")]
public class CharacterStats : ScriptableObject {
    public Stat maxHealth; // Maximum amount of health
    public int currentHealth { get; protected set; } // Current amount of health

    public Stat damage;
    public Stat armor;
    public event System.Action OnHealthReachedZero;
    protected void Awake ( ) {
        currentHealth = maxHealth.Value;
    }
    protected void OnEnable() {
        currentHealth = maxHealth.Value;
    }
    // Damage the character
    public void TakeDamage (int damage) {
        // Subtract the armor value - Make sure damage doesn't go below 0.
        damage -= armor.Value;
        damage = Mathf.Clamp (damage, 0, int.MaxValue);

        // Subtract damage from health
        currentHealth -= damage;
        // If we hit 0. Die.
        if (currentHealth <= 0) {
            if (OnHealthReachedZero != null)
                OnHealthReachedZero ( );
        }
    }

    // Heal the character.
    public void Heal (int amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth.Value);
    }
}
