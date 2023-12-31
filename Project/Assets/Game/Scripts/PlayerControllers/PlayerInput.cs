using System;
using UnityEngine;

namespace Game {
    
    internal sealed class PlayerInput : MonoBehaviour {

        // A TENER EN CUENTA: Habra que deshabilitar el Update cuando se este en los menus, 
        // porque el touch.Count cuenta todos los touch, sin importar el contexto del juego.
    
        internal delegate void _inputDelegate();
        internal static _inputDelegate OnInput;

        private static _inputDelegate _defaultInput;

        internal static TouchPhase CurrentTouchPhase;

        private void Update() {

            if (Input.touchCount == 0) {
                CurrentTouchPhase = TouchPhase.Canceled; // SUS
                return;
            }

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
