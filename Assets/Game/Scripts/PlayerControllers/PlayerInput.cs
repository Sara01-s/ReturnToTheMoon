using System;
using UnityEngine;

namespace Game {
    
    internal sealed class PlayerInput : MonoBehaviour {
    
        internal delegate void _inputDelegate();
        internal static _inputDelegate OnInput;

        private static _inputDelegate _defaultInput;

        internal static TouchPhase CurrentTouchPhase;

        private void Update() {

            if (Input.touchCount == 0) return;

            Touch touch = Input.GetTouch(0);

            CurrentTouchPhase = touch.phase;

            if (touch.phase == TouchPhase.Began) {
                OnInput?.Invoke();
            }
            if (touch.phase == TouchPhase.Stationary) {
                OnInput?.Invoke();
            }
        }

        internal static void SetDefaultInput(_inputDelegate inputDelegate) {
            _defaultInput = inputDelegate;  
            OnInput = _defaultInput;
        }

        internal static void Reset() {
            if (_defaultInput == null) return;

            OnInput = _defaultInput;
        }

    }
}
