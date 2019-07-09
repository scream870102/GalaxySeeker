/// <summary>struct define the basic information for an attack action</summary>
/// <remarks>include damage,cd,and detectRadius for circleCast</remarks>
[System.Serializable]
public struct AttackValue {
    public float Damage;
    public float CD;
    public float DetectRadius;
}
