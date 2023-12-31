using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class StealthEvent : QuickTimeEvent {
        
        [Header("StealthEvent Settings")]
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;

        [SerializeField] private float _delayDuration;
        [SerializeField] private float _bigDelayDuration;

        private TouchPhase _stationary = TouchPhase.Stationary; 
        private TouchPhase _moved = TouchPhase.Moved; 

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                PlayerInput.OnInput = Pressed;
                StartCoroutine(CO_InitQTE());
            }
        }

        private void Pressed() {
            // Otros eventos(?
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("Terminado el tiempo de preparacion...");
            Time.timeScale = DEFAULT_TIMESCALE;

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _eventDuration) {
                var currentTouchPhase = PlayerInput.CurrentTouchPhase;

                if (currentTouchPhase != _stationary && currentTouchPhase != _moved) {
                    StartCoroutine(CO_Lose());
                    yield break;
                }

                print("Hold!");
                yield return null;
            }

            PlayerInput.Reset();
            StartCoroutine(CO_Win());
        }

        protected override IEnumerator CO_Win() {
            _reactiveSpeed.Value = _playerSpeed.Slow;
            print("Well Done!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _delayDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _reactiveSpeed.Value = _playerSpeed.VerySlow;
            print("Bad!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _delayDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

    }
}
