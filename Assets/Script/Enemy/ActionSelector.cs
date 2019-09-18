using System;
using System.Collections.Generic;
public class ActionSelector {
    public static int SelectWithProbability (List<Action> chosen) {
        int sum = 0;
        for (int i = 0; i < chosen.Count; i++)
            sum += chosen [i].Probability;
        int selection = (new Random ( )).Next (sum);
        int count = 0;
        for (int i = 0; i < chosen.Count - 1; i++) {
            count += chosen [i].Probability;
            if (selection < count)
                return i;
        }
        return chosen.Count - 1;
    }
}
