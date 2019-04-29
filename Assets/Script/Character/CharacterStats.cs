using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>A class include all basic stats for character</summary>
/// <remarks>include maxHealth/health/armor/damage also have method Heal/TakeDamage</remarks>
[CreateAssetMenu(fileName = "New CharacterStats", menuName = "Character/CharacterStats")]
public class CharacterStats : ScriptableObject {
    /// <summary>Maximum amount of health</summary>
    public Stat maxHealth;
    /// <summary> current amount of health</summary>
    public int currentHealth { get; protected set; }
    /// <summary>damage of character</summary>
    public Stat damage;
    /// <summary>armor on the character</summary>
    public Stat armor;
    /// <summary>Event will call when character health reach zero</summary>
    public event System.Action OnHealthReachedZero;
    
    // when stats awake set current health to max health
    protected virtual void Awake ( ) {
        currentHealth = maxHealth.Value;
    }

    // when stats enable set current health to max health
    protected virtual void OnEnable() {
        currentHealth = maxHealth.Value;
    }

    /// <summary>Damage the character</summary>
    /// <param name="damage">how many damage taken</param>
    public void TakeDamage (int damage) {
        // Subtract the armor value - Make sure damage doesn't go below 0.
        damage -= armor.Value;
        damage = Mathf.Clamp (damage, 0, int.MaxValue);

        // Subtract damage from health
        currentHealth -= damage;
        // If we hit 0. Die. invoke event OnHealthReachedZero
        if (currentHealth <= 0) {
            if (OnHealthReachedZero != null)
                OnHealthReachedZero ( );
        }
    }

    /// <summary>Heal the character</summary>
    /// <param name="amount">how many point been heal</param>
    public void Heal (int amount) {
        // maxsure currenthealth less then maxHealth
        currentHealth += amount;
        currentHealth = Mathf.Clamp (currentHealth, 0, maxHealth.Value);
    }
}
