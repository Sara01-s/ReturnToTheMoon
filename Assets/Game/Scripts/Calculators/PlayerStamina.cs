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
        [SerializeField] private float _initialValue;

        private const float MAX_THRESHOLD = 100.0f;
        private const float NORMAL_THRESHOLD = 66.6f;
        private const float TIRED_THRESHOLD = 33.3f;
        private const float EXHAUSTED_THRESHOLD = 0.1f;
        private const float FATIGUED_THRESHOLD = 0.0f;

        private void Awake() {
            _reactiveStamina.Value = _initialValue;
        }

        private void OnEnable() {
            _reactiveStamina.Observable.Subscribe(UpdateStaminaState);
        }
        private void OnDisable() {
            _reactiveStamina.Observable.Dispose();
        }

        private void UpdateStaminaState(float stamina) {

            stamina = clamp(stamina, FATIGUED_THRESHOLD, MAX_THRESHOLD);

            if (Common.IsBetween(stamina, NORMAL_THRESHOLD, MAX_THRESHOLD)) {
                CurrentStaminaState = StaminaState.Normal;
            }
            else if (Common.IsBetween(stamina, TIRED_THRESHOLD, NORMAL_THRESHOLD)) {
                CurrentStaminaState = StaminaState.Tired;
            }
            else if (Common.IsBetween(stamina, EXHAUSTED_THRESHOLD, TIRED_THRESHOLD)) {
                CurrentStaminaState = StaminaState.Exhausted;
            }
            else if (Common.IsBetween(stamina, FATIGUED_THRESHOLD, EXHAUSTED_THRESHOLD)) {
                CurrentStaminaState = StaminaState.Fatigued;
            }

            Debug.Log("CurrentStamina: " + stamina + " Current StaminaState: " + CurrentStaminaState);
        }

    }
}
