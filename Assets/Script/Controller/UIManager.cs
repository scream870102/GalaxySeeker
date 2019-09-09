using System;

using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class UIManager {
    [SerializeField] Slider healthBar = null;
    GameObject healthObj = null;
    public void Init ( ) {
        if (healthBar != null) {
            healthObj = healthBar.gameObject;
        }
    }

    public void InitValue ( ) {
        GameManager.Instance.FindPlayer ( );
        GameManager.Instance.Player.Stats.OnHealthChanged += OnHealthValueChanged;
        healthBar.maxValue = GameManager.Instance.Player.Stats.maxHealth;
        healthBar.value = GameManager.Instance.Player.Stats.CurrentHealth;
    }

    void OnHealthValueChanged (float value) {
        healthBar.value = value;
    }

    public void EnableHealthUI (bool IsEnable = true) {
        healthObj.SetActive (IsEnable);
    }

}
