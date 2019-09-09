using UnityEngine;
namespace GalaxySeeker.UnityUtils {
    /// <summary>A class include all basic stats for character</summary>
    /// <remarks>include maxHealth/health/armor/damage also have method Heal/TakeDamage</remarks>
    [System.Serializable]
    public class CharacterStats {
        /// <summary>Maximum amount of health</summary>
        public float maxHealth;
        /// <summary> current amount of health</summary>
        [SerializeField]
        float currentHealth;
        public float CurrentHealth { get { return currentHealth; } protected set { currentHealth = value; } }
        /// <summary>Event will call when character health reach zero</summary>
        public event System.Action OnHealthReachedZero = null;
        public event System.Action<float> OnHealthChanged = null;
        /// <summary>Damage the character</summary>
        /// <param name="damage">how many damage taken</param>
        public void TakeDamage (float damage) {
            // Subtract the armor value - Make sure damage doesn't go below 0.
            //damage -= armor.Value;
            damage = UnityEngine.Mathf.Clamp (damage, 0, int.MaxValue);

            // Subtract damage from health
            currentHealth -= damage;
            //Debug.Log ("Health changed"+OnHealthChanged.Method);
            if (OnHealthChanged != null) {
                OnHealthChanged (currentHealth);
            }
            // If we hit 0. Die. invoke event OnHealthReachedZero
            if (currentHealth <= 0) {
                if (OnHealthReachedZero != null)
                    OnHealthReachedZero ( );
            }
        }
        public virtual void Init ( ) { CurrentHealth = maxHealth; }
        /// <summary>Heal the character</summary>
        /// <param name="amount">how many point been heal</param>
        public void Heal (int amount) {
            // maxsure currenthealth less then maxHealth
            CurrentHealth += amount;
            CurrentHealth = UnityEngine.Mathf.Clamp (CurrentHealth, 0, maxHealth);
            if (OnHealthChanged != null)
                OnHealthChanged (currentHealth);
        }
    }
}
