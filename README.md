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
## 190430
- 針對道具的使用按鈕做定義`Use`(e)
- 互動按紐`Interact`改成(q)
- 新增替換道具按紐`Switch`(r)
- 新增替換道具功能`PlayerEquipment.cs`
---
## 190516
- RopeSystem 大致上完成
    - 未完成
        * 調整落下後力消失的問題
        * 註解完成
        * 調整繩子無法碰撞的問題
    - 已完成
        * 玩家獲得繩子道具以後 新增DistanceJoint2D到玩家身上
        * 繩子LineRenderer新增
        * 可以透過移動按鍵在空中擺蕩
- `PlayerStats.cs`新增Stat:**swingForce** 可以用來調整玩家使用繩所在空中擺蕩的力值
- 修正`PlayerMovement.cs`中造成Rope擺蕩中重力的異常(主要是不能令`rb.velocity=new Vector2(某值,rb.velocity.y);`由於值不停維持在同一個frame裡的velocity.y所以物理引擎所帶來的重力影響就被弱化了)
---
## 190518
- 調整從空中繩子脫落後 身上X方向的力消失的問題`PlayerMovement.cs`
- 繩子本身不會有碰撞體 因此必須要再地圖上設計 防止玩家穿過
- 開啟distanceJoint2D的enableCollision否者角色本身會穿過地形
- 修正還在使用A道具的同時 就可以切換B道具並使用的
- 整理目前為止的註解
---
## 190523
- 完成噴射背包系統'Jetpack.cs'
    * 噴射背包的cd還有force有待調整
- 加上註解
- 調整`PlayerEquipment.cs`中的部份結構以符合Jetpack
- 調整`ItemPickup.cs`
    * 將其改成只要掛上ItemPickup同時擁有trigger類型的collider以及item類別在上面 便可以讓物體可以被撿起來
    * 只要物體被撿起會更改其Parent到角色並同時移除身上的ItemPickUp.cs跟collider
    * 注意collider是用getComponent獲取 因此如果有兩個collider在身上需要透過手動做指定
---
## 190524 
- 修改噴色背包系統'Jetpack.cs'
    * 將背包x方向會維持原訂速度不停前進的問題修正
    * 把裝上背高的force設定移到PlayerMovemetn.cs中
- 修改PlayerStats部份數值運作
    * `swingForce`&`flyingForce`會在撿拾到道具的時候被定義 預設值為0

### 未完成
- 修改xmind `Galaxy Seeker.xmind`
- 死亡的動作 `Player.cs`
- `PlayerAnimation.cs`
- 實現移除道具的功能`PlayerEquipment.cs`

---