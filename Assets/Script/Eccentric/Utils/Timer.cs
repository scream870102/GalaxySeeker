namespace Eccentric.Utils {
    /// <summary>a countdown timer easy to use</summary>
    /// <remarks>call method Reset to reset timer and call property IsFinshed to check if countdown finished</remarks>
    public class Timer {
        float timeSection;
        float timer;
        /// <summary>remaining time until the countdown end</summary>
        public float Remain {
            get {
                float offset = timer - UnityEngine.Time.time;
                if (offset >= 0f)
                    return offset;
                else return 0f;
            }
        }
        /// <summary>return the cd range from 0 to 1 0 means timer finished </summary>
        public float Remain01 {
            get {
                return Remain / timeSection;
            }
        }
        /// <summary>if this countdown finished or not</summary>
        public bool IsFinished {
            get {
                if (timer <= UnityEngine.Time.time)return true;
                else return false;
            }
        }

        public Timer (float timeSection = 0f, bool CanUseFirst = true) {
            this.timeSection = timeSection;
            if (!CanUseFirst)
                Reset ( );
            else
                this.timer = 0f;
        }
        /// <summary>Reset countdown timer with default setting</summary>
        public void Reset ( ) {
            timer = UnityEngine.Time.time + timeSection;
        }
        /// <summary>reset countdown timer with new timeSection</summary>
        public void Reset (float timeSection) {
            this.timeSection = timeSection;
            timer = UnityEngine.Time.time + timeSection;
        }

    }
}
