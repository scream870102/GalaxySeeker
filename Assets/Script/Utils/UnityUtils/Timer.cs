namespace Eccentric.UnityUtils {
    public class CountdownTimer {
        float timeSection;
        float timer;
        public float Remain {
            get {
                float offset = timer - UnityEngine.Time.time;
                if (offset >= 0f)
                    return offset;
                else return 0f;
            }
        }
        public bool IsFinished {
            get {
                if (timer <= UnityEngine.Time.time) return true;
                else return false;
            }
        }
        public CountdownTimer (float timeSection=0f) {
            this.timeSection = timeSection;
            Reset ( );
        }

        public void Reset ( ) {
            timer = UnityEngine.Time.time + timeSection;
        }
        public void Reset (float timeSection) {
            timer = UnityEngine.Time.time + timeSection;
        }

    }
}
