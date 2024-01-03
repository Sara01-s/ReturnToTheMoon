using UnityEngine;
using System;

namespace Game {
    
    internal sealed class PlayerInput : MonoBehaviour {

        // A TENER EN CUENTA: Habra que deshabilitar el Update cuando se este en los menus, 
        // porque el touch.Count cuenta todos los touch, sin importar el contexto del juego.
    
        internal static Action OnInput;
        private static Action DefaultInput;

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

        internal static void SetDefaultInputCallback(Action inputCallback) {
            DefaultInput = inputCallback;  
            OnInput = DefaultInput;
        }

        internal static void Reset() {
            if (DefaultInput == null) {
            	OnInput = DefaultInput;
			}
        }

    }
}
