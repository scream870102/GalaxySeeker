using System;

using GalaxySeeker.Item;

using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class UIManager {
    [SerializeField] ItemRef [] refs;
    [SerializeField] GameObject healthObj = null;
    [SerializeField] RectTransform healthBar = null;
    [SerializeField] Image itemUp = null;
    [SerializeField] Image itemDown = null;
    [SerializeField] RectTransform airPackBar = null;
    [SerializeField] GameObject airPackUI = null;
    float maxHealth = 0f;
    float maxHealthBar = 0f;
    Jetpack packRef = null;
    public void Init ( ) {
        if (healthBar != null) {
            healthObj = healthBar.gameObject;
        }
    }

    public void InitValue ( ) {
        GameManager.Instance.FindPlayer ( );
        GameManager.Instance.Player.Stats.OnHealthChanged += OnHealthValueChanged;
        maxHealth = GameManager.Instance.Player.Stats.MaxHealth;
        maxHealthBar = healthBar.sizeDelta.y;
        OnHealthValueChanged (GameManager.Instance.Player.Stats.CurrentHealth);
        //subscribe the event for cd and itemChanged
        GameManager.Instance.Player.Equipment.OnCdChanged += OnCDChanged;
        GameManager.Instance.Player.Equipment.OnItemChanged += OnItemChanged;
    }

    void OnHealthValueChanged (float value) {
        healthBar.sizeDelta = new Vector2 (healthBar.sizeDelta.x, maxHealthBar * value / maxHealth);
    }

    public void EnableHealthUI (bool IsEnable = true) {
        healthObj.SetActive (IsEnable);
    }

    void OnItemChanged (EItemType type, Item item) {
        //enable or disable the jetpack ui
        if (type == EItemType.JETPACK) {
            airPackUI.SetActive (true);
            packRef = item as Jetpack;
            packRef.OnGasChanged += OnGasChanged;
        }
        else {
            airPackUI.SetActive (false);
            if (packRef != null) {
                packRef.OnGasChanged -= OnGasChanged;
                packRef = null;
            }
        }
        //set the ui from item type
        foreach (ItemRef itemPair in refs) {
            if (itemPair.itemType == type) {
                itemUp.sprite = itemPair.sprite;
                itemDown.sprite = itemPair.sprite;
                return;
            }
        }
    }

    void OnCDChanged (float remain) {
        itemUp.fillAmount = 1.0f - remain;
    }

    void OnGasChanged (float remain) {
        airPackBar.sizeDelta = new Vector2 (airPackBar.sizeDelta.x, remain * 100f);
    }

    [System.Serializable]
    class ItemRef {
        public EItemType itemType;
        public Sprite sprite;
    }

}
