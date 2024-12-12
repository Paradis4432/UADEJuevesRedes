using UnityEngine;

namespace Tools {
    public abstract class AbstractUI<T> : MonoBehaviour where T : AbstractUI<T> {
        protected virtual void Awake() {
            Reflection.ValidateClassFields(GetObj());
        }

        /**
         * no super(this) in constructor is wild, stupid c#
         */
        protected abstract T GetObj();
    }
}