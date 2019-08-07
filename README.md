# GalaxySeeker
---
## 開發日誌 DevLog
- [190529-物件池 開發日誌](https://eccentric0102.wordpress.com/2019/05/29/190529-【galaxy-seeker】devlog物件池/)
- [190531-Stats 開發日誌](https://eccentric0102.wordpress.com/2019/06/01/190531-%e3%80%90galaxy-seeker%e3%80%91devlogstats/)
- [190709-對話系統 開發日誌](https://eccentric0102.wordpress.com/2019/07/09/190709-【galaxy-seeker】devlog對話系統/)
---
### 未完成
- 死亡的動作 `Player.cs`
- `PlayerAnimation.cs`
- 實現移除道具的功能`PlayerEquipment.cs`
- 攝影機
- 將主角的component修改成不繼承自Mono
- 主畫面的Option選項
- 主角死亡後的處理
- Laiter星打完BOSS以後的動畫以及後續發展
- 對話系統待修正
---
## 190717
- 完成KingCannibalFlower
    - 包含攻擊模式
        - Special
            - Vine Sting
            - Vine Scratch
        - Normal
            - Bite
            - Needle
            - Vine
    - 設定所有攻擊的動畫
    - 根據角色方向改變攻擊方向
    - 可以設定普通攻擊跟特殊攻擊的出招頻率
- 新增以下動畫
    - Jellyfish
    - AirStingray
    - Stingray
- 修改FreeTaceMove會跟對象重合的問題
- 新增`Timer` 簡易型的CountdownTimer
- 新增`Render` 可以根據boolean設定角色動畫翻轉

---
## 190714
- Unity版本升級成2019.1.10.f1
- 完成RedAirStingray
- 新增IMove
    - 作為移動相關的基礎類別
    - 包含以下
        - bUseDeltaTime 定義這個移動在計算使用deltaTime 還是 fixedDeltaTime
        - bFacingRight 透過新舊位置計算出角色應該面對哪一個方向
        - GetNextPos 外部透過這個方法來獲取下一個座標
- 新增`FreeTraceMode.cs` 無視任何障礙物 筆直的往目標前進的移動模式
- 新增`PingPongMove.cs` 在設定好的垂直高度及水平距離內做來回移動
    - 可以透過修改IsFacingRight來使其提早轉彎
- 新增`RangeRandomMove.cs`在設定好的範圍內 從初始位置找到目標位置並移動 抵達目標位置後會尋找新的目標位置
    - 可以透過FindNewTargetPos來提早尋找新的位置
- 修改`JellyFishMovement.cs`
- 修改`AirStingrayMovement.cs`

---
## 190712
- 完成AirStingray
- 新增static class Math
    - 新增Between 確認一個值否介在某兩個值中間
    - 新增ChoseDueToProbability 回傳兩者其一 根據兩者各自被選中的機率 會轉換兩者的機率總和為100%
- 修改FindTarget成Physics2D
    - 新增OverlapCircle 尋找是否有符合目標存在Circle內
    - 新增CircleCast 尋找是否有符合的目標存在CircleCast內
    - [Different between CircleCast and OverlapCircle](https://answers.unity.com/questions/834875/what-is-the-difference-between-circlecast-and-over.html)
    - 新增IsAbove/Under/Right/Left 來確認一個點是否存在指定點的方向上
- 新增SingletonMonoBehavior 可以透過繼承這個類別 完成MonoBehavior的單例模
式
- 新增DisableAllComponents in `Enemy.cs`
    - 當角色被Disable 會Disable 所有 CharacterComponent
- 新增Disable in `CharacterComponent.cs`
    - 當Component被Disable 會呼叫Method Disable 可以用來設定結束動作

---
## 190709
- 新增`SingletonMonoBehavior.cs`
- 修改資料夾結構

---
## 190708
- Unity版本升級成2019.1.9f1
- 完成 CannibalFlower的攻擊
    - Needle針刺攻擊
    - Bite 咬擊攻擊
- 移除類別`PlayerStat` & `EnemyStat` & `Stat`
- 補上註解
- 新增Namespace
    - Eccentric.UnityModel
    - Eccentric.UnityModel.Attack
    - Eccentric.UnityModel.Toolkit
    - Eccentric.Interface
- 新增類別`CircleAreaAttack.cs`
    - 繼承實作 Eccentric.Interface.IAttack
    - 定義一個圓形偵測範圍的攻擊模式
- 新增Static類別`FindTarget`
    - NameSpace:Eccentric.UnityModel.Physics2D
    - 包含static methods 可以用來尋找對象是否在範圍內
- 修改物件池`ObjectPool.cs`

---
## 190703
- 新增GlobalProps可以設定全域的相關設定
- 新增FindTarget類別 用來尋找目標
    - 新增CircleCast 可以透過圓形區域尋找目標
- 修改`JellyfishMovement.cs` 將尋找目標的方法修改成使用FindTarget
- 修改`JellyfishAttack.cs` 將尋找目標的方法修改成使用FindTarget
- 新增敵人 CannibalFlower 作為LaiterBoss
    - 簡單定義攻擊
- Laiter星中 必要NPC對話 以及關底Boss的流程定義完畢
     - 可以透過拖曳 來定義哪幾個NPC為必要對話的
     - 當所有NPC對話完畢 BOSS前的牆壁會關閉
     - 打完BOSS會回到起始畫面

---
## 190629
- 完成單向的對話系統
- 完成基本NPC的對話功能
- 將對話系統掛在GameManger內部
- 補上註解
- 對話系統基本UI完成
    - 將角色的座標點直接轉換成螢幕座標 而非Viewport座標
    - RectTransform 參考文章
        - [Unity UGUI 原理篇(三)：RectTransform](http://www.arkaistudio.com/blog/2016/05/02/unity-ugui-原理篇三：recttransform/)
        - [成為 UGUI 的排版大師 – 一次精通 RectTransform](http://blog.fourdesire.com/2018/07/03/成為-ugui-的排版大師-一次精通-recttransform/)

---
## 190628
- 完成船艦畫面選擇星球功能
- 完成主畫面選擇功能
    - 選項透過struct 將 UI元素跟UnityEvent包裝起來 方便從Inspector中選擇對應的動作
        ```c#
        struct Button {
            public Text text;
            public UnityEvent action;
        }
        ```
- 新增`EnableComponent<T>(bool value)`方法
    - 透過Generic可以直接從呼叫階段決定要關掉哪一個行別的PlayerComponent
    - T只屬於類別 不屬於物件 必須利用[GetType](https://docs.microsoft.com/zh-tw/dotnet/api/system.type.gettype?view=netframework-4.8)動態找到其類別 並儲存成[Type](https://docs.microsoft.com/zh-tw/dotnet/api/system.type?view=netframework-4.8)物件
    - 透過[Equality==運算子](https://docs.microsoft.com/zh-tw/dotnet/api/system.type.op_equality?view=netframework-4.8)或是[Equals](https://docs.microsoft.com/zh-tw/dotnet/api/system.type.equals?view=netframework-4.8) 來比較兩個Type 是否相等
- 新增`EnableComponents`方法
- 補上註解
- 主畫面選項 加上簡易的顏色改變回饋
- Unity版本升級成2019.1.8.f1

---
## 190622
- 優化Jellyfish attack 中circle cast的使用頻率
- 修正使用jetPack空中不會轉向的問題
- 將ObjectPoolItem移除
- 需要被物件池管理的物件繼承IObjectPoolItem(Interface)
    - GetComponent可以尋找**Interface**
        ```c#
        GameObject.Instantiate (pooledObject).GetComponent<IObjectPoolItem> ( );
        ```
    - 需要被物件池管理的物件需要實作IObjectPoolItem中的
        - ObjectPool Pool { get; set; }
        - GameObject gameObject { get; }
        - void Recycle ( );
        - void Init ( );
- Unity版本升級成2019.1.7.f1
- 水母移動的動畫可以透過子物件移動相對位置來達成
- 完成註解

---
## 190621
- Jellyfish對主角的追蹤
- Jellyfish任意移動的修正
- Jellyfish的攻擊方式
- 完成註解
- 優化Jellyfish attack 中circle cast的使用頻率

---
## 190608
- 修改初步的敵人系統 讓所有的Component 都不繼承自MonoBehavior
- 著手寫敵人Jellyfish
- 初步完成Jellyfish的移動
- 補上註解
- Unity版本升級成2019.1.5f1

---
## 190530
- 替子彈系統加上Reload的功能
- 初步的敵人系統可以被子彈攻擊死亡 然後會呼叫`Dead`
- 修正裝備系統再沒有任何道具時點擊交換道具的存取錯誤
- 替`PlayerShooting`加上設定每一發子彈傷害的部份 位於`Fire`
- 補上註解

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
## 190524 
- 修改噴色背包系統'Jetpack.cs'
    * 將背包x方向會維持原訂速度不停前進的問題修正
    * 把裝上背高的force設定移到PlayerMovement.cs中
- 修改PlayerStats部份數值運作
    * `swingForce`&`flyingForce`會在撿拾到道具的時候被定義 預設值為0
- 完成DASH球鞋`SpargeShoes.cs`
    * 對於角色力的施加直接寫在shoes上 而非Playermovement上

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
## 190518
- 調整從空中繩子脫落後 身上X方向的力消失的問題`PlayerMovement.cs`
- 繩子本身不會有碰撞體 因此必須要再地圖上設計 防止玩家穿過
- 開啟distanceJoint2D的enableCollision否者角色本身會穿過地形
- 修正還在使用A道具的同時 就可以切換B道具並使用的
- 整理目前為止的註解

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
## 190430
- 針對道具的使用按鈕做定義`Use`(e)
- 互動按紐`Interact`改成(q)
- 新增替換道具按紐`Switch`(r)
- 新增替換道具功能`PlayerEquipment.cs`

---
## 190429
- 測試現有所有的code 運行
- 修改item.cs從繼承ScriptableObject至MonoBehavior
- AddItem從獲取ScriptableObject ref變成Add new component
    * 因為Item 原先掛在ItemPick 在 item 被拾起來 會destroy整個gameObject因此 必須將item移到Player身上
    * 不使用transform.parent 因為無法跨gameObject使用(待驗證)
- 替Item 新增event OnItemUsed 當物品使用完畢 通知PlayerInventory
- 完成現有code註解

---
## 190425
- Player/PlayerComponent/PlayerEquipment/PlayerMovement/PlayerStats/Stat 註解

---
## 190419
- item/itemPickUp 與 PlayerEquipment的互動
- 新增 character/characterStats IInteractable/Item/ItemPickUp 註解
- 新增PlayerComponent 主要定義Player 也就是parent不存在時 Update/FixedUpdate將直接return 而不作用
- 將 PlayerMovement/PlayerEquipment 修改成繼承自PlayerComponent 