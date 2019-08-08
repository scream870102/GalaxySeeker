using System;

using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class UIManager {
    [SerializeField] Slider healthBar = null;
    public void Init (float maxHealth) {
        if (healthBar != null) {
            healthBar.maxValue = maxHealth;
            GameManager.Instance.Player.Stats.OnHealthChanged += OnHealthValueChanged;
            healthBar.value = GameManager.Instance.Player.Stats.CurrentHealth;
        }
    }

    void OnHealthValueChanged (float value) {
        healthBar.value = value;
    }

}
