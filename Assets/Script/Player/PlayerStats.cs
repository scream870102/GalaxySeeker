using System.Collections;
using System.Collections.Generic;

using UnityEngine;
/// <summary>playerStats is inherit from characterStats</summary>
///<remarks>You can set walkSpeed/jumpForce/attackPoint/airSpeed/itemSpace in this class</remarks>
[System.Serializable]
[CreateAssetMenu (fileName = "New PlayerStats", menuName = "Character/PlayerStats")]
public class PlayerStats : CharacterStats {
    ///<summary>define how fast does player walk on the ground</summary>
    public Stat walkSpeed;
    ///<summary>define how many force will add when player jump</summary>
    public Stat jumpForce;
    ///<summary>define how many damage will cause when player attack enemy</summary>
    public Stat attackPoint;
    ///<summary>define how fast dose player move when player isn't on the ground</summary>
    public Stat airSpeed;
    ///<summary>how many item can player equip</summary>
    public Stat itemSpace;
    ///<summary> define how many force wiil add to player when player is swing with rope
    public Stat swingForce;
}
