using UnityEngine;
namespace Eccentric.UnityUtils {
    /// <summary>derived this class then you will got a monobehavior implement singleton</summary>
    public class SingletonMonoBehavior<T> : MonoBehaviour where T : MonoBehaviour {
        static T instance;

        public static T Instance {
            get { return instance ?? (instance = FindObjectOfType (typeof (T)) as T); }
            set { instance = value; }
        }

        protected virtual void Awake ( ) {
            instance = this as T;
            DontDestroyOnLoad(this);
        }

        protected virtual void OnDestroy ( ) {
            instance = null;
        }
    }
}
