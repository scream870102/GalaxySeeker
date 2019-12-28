namespace GalaxySeeker.UI {
    using UnityEngine.UI;
    using UnityEngine;
    [System.Serializable]
    public abstract class BaseUIController {
        [SerializeField] Canvas parent = null;
        public Canvas Parent => parent;
        bool bEnable = true;
        public bool Enable {
            set {
                bEnable = value;
                if (parent)
                    parent.enabled = value;
            }
            get => bEnable;
        }
        public abstract void Init ( );
        protected abstract void Destroy ( );
        ~BaseUIController ( ) {
            Destroy ( );
        }

    }
}
