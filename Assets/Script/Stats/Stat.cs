using System.Collections.Generic;

[System.Serializable]
public class Stat {
    ///<summary>Starting value</summary>
    public int baseValue;
    // Keep a list of all the modifiers on this stat
    private List<int> modifiers = new List<int> ( );

    /// <summary>Add all modifiers together and return the result</summary>
    public int Value {
        get {
            int finalValue = baseValue;
            foreach (int modifier in modifiers)
                finalValue += modifier;
            return finalValue;
        }
    }

    ///<summary> Add a new modifier to the list</summary>
    public void AddModifier (int modifier) {
        if (modifier != 0)
            modifiers.Add (modifier);
    }

    ///<summary> Remove a modifier from the list</summary>
    public void RemoveModifier (int modifier) {
        if (modifier != 0)
            modifiers.Remove (modifier);
    }

}
