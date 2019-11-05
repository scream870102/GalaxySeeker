using Eccentric.Utils;

using UnityEngine;
[System.Serializable]
public class Action {
    [SerializeField] string trigger = "";
    [SerializeField] int probability = 0;
    [SerializeField] float coolDown = 0f;
    [SerializeField] float distance = 0f;
    public string Trigger { get { return trigger; } }
    public int Probability { get { return probability; } set { if (value <= 100 && value >= 0) probability = value; } }
    public float CoolDown { get { return coolDown; } set { if (value >= 0f) coolDown = value; } }
    public Timer Timer { get; protected set; }
    public bool IsCanUse { get { return this.Timer.IsFinished; } }
    public float Distance { get { return distance; } }
    public void ResetCD (bool IsNewCD = false, float timeSection = 0f) {
        if (IsNewCD) this.Timer.Reset (timeSection);
        else this.Timer.Reset ( );
    }

    public void InitAction ( ) {
        this.Timer = new Timer (this.CoolDown);
    }
}
