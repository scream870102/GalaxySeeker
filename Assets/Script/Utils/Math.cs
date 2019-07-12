using System;
namespace Eccentric {
    public static class Math {
        /// <summary>check if a value between the two value or not</summary>
        /// <remarks>will return true if the value equal to a or b</remarks>
        /// <param name="value">the value you want to check</param>
        public static bool Between (float value, float a, float b) {
            if (a > b) {
                if (value <= a && value >= b) return true;
                else return false;
            }
            else {
                if (value >= a && value <= b) return true;
                else return false;
            }
        }

        /// <summary>return the option due to the percentage</summary>
        /// <remarks>will transfer the total of two probability to 100% linearly</remarks>
        /// <param name="option1">first option</param>
        /// <param name="option2">second option</param>
        /// <param name="probability1">the probability of option1</param>
        /// <param name="probability2">the probability of option2</param>
        public static T ChosenDueToProbability<T> (in T option1, in T option2, float probability1, float probability2) {
            if (probability1 + probability2 != 1f) {
                float tmp = 1f / (probability1 + probability2);
                probability1 *= tmp;
                probability2 *= tmp;
            }
            Random rand = new Random (DateTime.Now.Millisecond);
            float percentage = (float) rand.NextDouble ( );
            if (percentage <= probability1) return option1;
            else return option2;
        }
    }
}
