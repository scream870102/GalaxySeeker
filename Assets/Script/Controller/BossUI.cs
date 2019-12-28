using System;

using GalaxySeeker.UI;

using UnityEngine;
[System.Serializable]
public class BossUI : BaseUIController {
    [SerializeField] GameObject healthObj = null;
    [SerializeField] RectTransform healthBar = null;
    [SerializeField] Character boss = null;
    CharacterStats bossStats = null;
    float maxHealth = 0f;
    float maxHealthBar = 0f;
    override public void Init ( ) {
        bossStats = boss.Stats;
        if (healthBar != null) {
            healthObj = healthBar.gameObject;
        }
        bossStats.OnHealthChanged += OnHealthValueChanged;
        maxHealth = bossStats.MaxHealth;
        maxHealthBar = healthBar.sizeDelta.y;
        OnHealthValueChanged (bossStats.CurrentHealth);
    }
    override protected void Destroy ( ) {
        bossStats.OnHealthChanged -= OnHealthValueChanged;
    }

    void OnHealthValueChanged (float value) {
        healthBar.sizeDelta = new Vector2 (healthBar.sizeDelta.x, maxHealthBar * value / maxHealth);
    }

    public void EnableHealthUI (bool IsEnable = true) {
        healthObj.SetActive (IsEnable);
    }

}
