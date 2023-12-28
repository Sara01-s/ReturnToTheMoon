using UnityEngine;

namespace Game {
    
    internal sealed class PlayerInput : MonoBehaviour {
    
        internal delegate void _onInput(Touch touch);

        internal static _onInput OnInput;

        private void Update() {

            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began) {
                OnInput?.Invoke(touch);
            }
            if (touch.phase == TouchPhase.Stationary) {
                OnInput?.Invoke(touch);
            }
        }

    }
}
