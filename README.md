# GalaxySeeker
---
## 開發日誌 DevLog
[190529-物件池開發日誌](https://eccentric0102.wordpress.com/2019/05/29/190529-【galaxy-seeker】devlog物件池/)
[190531-Stats開發日誌](https://eccentric0102.wordpress.com/2019/06/01/190531-%e3%80%90galaxy-seeker%e3%80%91devlogstats/)
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
- 完成DASH球鞋`SpargeShoes.cs`
    * 對於角色力的施加直接寫在shoes上 而非Playermovement上
---
## 190525
- 完成子彈系統部份
    * 運用`ObjectPool.cs`來管理子彈的數量
    * `Bullet.cs`繼承自`ObjectPoolItem.cs`
    * 尚未寫對敵人造成傷害的部份
- 修改`CharacterStats.cs`、`PlayerStats.cs`
    * 不再繼承自`ScriptableObject`
    * 必須在需要使用Stats的地方直接new物件出來並設定
- 修改`PlayerComponent.cs`
    * 修正Component在沒有Parent時仍然能夠執行的問題
    * Tick = Update ||  FixedTick = FixedUpdate
- 加上目前的所有註解
---
## 190529
- 替子彈加上視覺回饋(ParticleSystem)
- 加上子彈的CD系統`PlayerShooting.cs`
- 補上註解
- 角色會根據面對的方向修改其scale達到水平翻轉
- 將子彈的物件池Parent移出角色 以解決角色換方向時 子彈也會跟著換方向的問題
- 噴射背包跟DASH球鞋的視覺回饋
- Unity版本升級成2019.1.4f1
---
## 190530
- 替子彈系統加上Reload的功能
- 初步的敵人系統可以被子彈攻擊死亡 然後會呼叫`Dead`
- 修正裝備系統再沒有任何道具時點擊交換道具的存取錯誤
- 替`PlayerShooting`加上設定每一發子彈傷害的部份 位於`Fire`
- 補上註解
---
## 190608
- 修改初步的敵人系統 讓所有的Component 都不繼承自Monobehavior
- 著手寫敵人Jellyfish
- 初步完成Jellyfish的移動
- 補上註解
- Unity版本升級成2019.1.5f1
### 未完成
- 修改xmind `Galaxy Seeker.xmind`
- 死亡的動作 `Player.cs`
- `PlayerAnimation.cs`
- 實現移除道具的功能`PlayerEquipment.cs`
- 攝影機
- Jellyfish對主角的追蹤
- Jellyfish的攻擊
- 將主角的component修改成不繼承自Mono
---