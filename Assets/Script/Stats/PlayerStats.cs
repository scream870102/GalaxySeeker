using System.Collections;
using System.Collections.Generic;
/// <summary>playerStats is inherit from characterStats</summary>
///<remarks>You can set walkSpeed/jumpForce/attackPoint/airSpeed/itemSpace in this class</remarks>
[System.Serializable]
public class PlayerStats : CharacterStats {
    ///<summary>define how fast does player walk on the ground</summary>
    public Stat walkSpeed;
    ///<summary>define how many force will add when player jump</summary>
    public Stat jumpForce;
    ///<summary>define how fast dose player move when player isn't on the ground</summary>
    public Stat airSpeed;
    ///<summary>how many item can player equip</summary>
    public int itemSpace;
    ///<summary> define how many force wiil add to player when player is swing with rope
    public Stat swingForce;
    /// <summary> define how many force will add to player when playe is flying with jetPack</summary>
    public Stat flyingGasForce;
    /// <summary>the basic damage of player</summary>
    public Stat damage;
    /// <summary>Heal the character</summary>
    /// <param name="amount">how many point been heal</param>
    public void Heal (int amount) {
        // maxsure currenthealth less then maxHealth
        CurrentHealth += amount;
        CurrentHealth = UnityEngine.Mathf.Clamp (CurrentHealth, 0, maxHealth);
    }
    /// <summary>Init all value</summary>
    public override void Init ( ) {
        CurrentHealth = maxHealth;
    }

}
