﻿using System.Collections.Generic;

[System.Serializable]
public class Stat {

    public int baseValue; // Starting value

    // Keep a list of all the modifiers on this stat
    private List<int> modifiers = new List<int> ( );

    // Add all modifiers together and return the result
    public int Value {
        get {
            int finalValue = baseValue;
            foreach (int modifier in modifiers)
                finalValue += modifier;
            return finalValue;
        }
    }

    // Add a new modifier to the list
    public void AddModifier (int modifier) {
        if (modifier != 0)
            modifiers.Add (modifier);
    }

    // Remove a modifier from the list
    public void RemoveModifier (int modifier) {
        if (modifier != 0)
            modifiers.Remove (modifier);
    }

}
