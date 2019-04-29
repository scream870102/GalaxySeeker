# GalaxySeeker
---
## 190419
- item/itemPickUp 與 PlayerEquipment的互動
- 新增 character/characterStats IInteractable/Itme/ItemPickUp 註解
- 新增PlayerComponent 主要定義Player 也就是parent不存在時 Update/FixedUpdate將直接return 而不作用
- 將 PlayerMovement/PlayerEqipment 修改成繼承自PlayerComponent 
---
## 190425
- Player/PlayerComponent/PlayerEquipment/PlayerMovement/PlayerStats/Stat 註解
---
## 190429
- 測試現有所有的code 運行
- 修改item.cs從繼承ScriptableObject至MonoBehavior
- AddItem從獲取ScriptableObject ref變成Add new component
    * 因為Item 原先掛在ItemPick 在 item 被拾起來 會destroy整個gameObject因此 必須將item移到Player身上
    * 不使用transform.parent 因為無法跨gameObject使用(待驗證)
- 替Item 新增event OnItemUsed 當物品使用完畢 通知PlayerEvenetory
- 完成現有code註解
---
### 未完成
- 針對道具的使用按鈕做定義 `PlayerEquipment.cs`
- 修改xmind `Galaxy Seeker.xmind`
- 死亡的動作 `Player.cs`
- Rope的實現 `Rope.cs`
- `PlayerAnimation.cs`

---