using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New PlayerStats", menuName = "Character/PlayerStats")]
public class PlayerStats : CharacterStats
{
    public Stat walkSpeed;
    public Stat jumpForce;
    public Stat attackPoint;
    public Stat airSpeed;
    public Stat itemSpace;
}
