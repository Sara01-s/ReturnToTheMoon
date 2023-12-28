using static Unity.Mathematics.math;
using UnityEngine;
using UniRx;
using VVT;

namespace Game {

    internal enum StaminaState {
        Normal = 0,
        Tired = 1,
        Exhausted = 2,
        Fatigued = 3
    }

    internal sealed class PlayerStamina : MonoBehaviour {

        internal StaminaState CurrentStaminaState;

        [SerializeField] private FloatResourceRx _reactiveStamina;
        [SerializeField] private float NORMAL_THRESHOLD = 66.6f;
        [SerializeField] private float TIRED_THRESHOLD = 33.3f;
        [SerializeField] private float EXHAUSTED_THRESHOLD = 0.1f;
        [SerializeField] private float FATIGUED_THRESHOLD = 0.0f;

        private void OnEnable() {
            _reactiveStamina.Observable.Subscribe(UpdateStaminaState);
        }
        private void OnDisable() {
            _reactiveStamina.Observable.Dispose();
        }

        private void UpdateStaminaState(float stamina) {

            stamina = clamp(stamina, 0.0f, 100.0f);

            if (Common.IsBetween(stamina, 66.6f, 100.0f)) {
                CurrentStaminaState = StaminaState.Normal;
            }
            else if (Common.IsBetween(stamina, 33.3f, 66.6f)) {
                CurrentStaminaState = StaminaState.Tired;
            }
            else if (Common.IsBetween(stamina, 0.1f, 33.3f)) {
                CurrentStaminaState = StaminaState.Exhausted;
            }
            else if (Common.IsBetween(stamina, 0.0f, 0.1f)) {
                CurrentStaminaState = StaminaState.Fatigued;
            }

            Debug.Log("Current StaminaState: " + CurrentStaminaState);
        }

    }
}
