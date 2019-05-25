using System.Collections;
using System.Collections.Generic;
/// <summary>A class include all basic stats for character</summary>
/// <remarks>include maxHealth/health/armor/damage also have method Heal/TakeDamage</remarks>
[System.Serializable]
public class CharacterStats {
    /// <summary>Maximum amount of health</summary>
    public int maxHealth;
    /// <summary> current amount of health</summary>
    [UnityEngine.SerializeField]
    int currentHealth;
    public int CurrentHealth { get { return currentHealth; } protected set { currentHealth = value; } }
    /// <summary>damage of character</summary>
    public Stat damage;
    /// <summary>armor on the character</summary>
    public Stat armor;
    /// <summary>Event will call when character health reach zero</summary>
    public event System.Action OnHealthReachedZero;
    /// <summary>Damage the character</summary>
    /// <param name="damage">how many damage taken</param>
    public void TakeDamage (int damage) {
        // Subtract the armor value - Make sure damage doesn't go below 0.
        damage -= armor.Value;
        damage = UnityEngine.Mathf.Clamp (damage, 0, int.MaxValue);

        // Subtract damage from health
        currentHealth -= damage;
        // If we hit 0. Die. invoke event OnHealthReachedZero
        if (currentHealth <= 0) {
            if (OnHealthReachedZero != null)
                OnHealthReachedZero ( );
        }
    }
    public virtual void Init ( ) { }
}
