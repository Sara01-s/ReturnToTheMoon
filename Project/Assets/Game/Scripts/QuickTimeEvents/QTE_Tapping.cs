using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class QTE_Tapping : QuickTimeEvent {

        [Header("QTE_Tapping Settings")]
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;
        
        [SerializeField]  private int _countGoal;
        [SerializeField]  private float _boostDuration;
        [SerializeField]  private float _delayDuration;

        private bool _tapped;

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                PlayerInput.OnInput = Tapped;

                StartCoroutine(CO_InitQTE());
            }
        }

        private void Tapped() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
            _tapped = true;
            // Otras cosas(?
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("TapTapTap!");
            
            var startTime = Time.unscaledTimeAsDouble;
            var tapCounter = 0;

            while ((Time.unscaledTimeAsDouble - startTime) <= _eventDuration) {
                
                if (_tapped) {
                    tapCounter++;
                    _tapped = false;
                    print("TapCounter: " + tapCounter);
                }
                
                yield return null;
            }

            print("Stop!");
            PlayerInput.Reset();
            Time.timeScale = DEFAULT_TIMESCALE;
            if (tapCounter >= _countGoal) StartCoroutine(CO_Win());
            if (tapCounter < _countGoal) StartCoroutine(CO_Lose());
        }

        protected override IEnumerator CO_Win() {
            _reactiveSpeed.Value = _playerSpeed.Fast;
            print("Logrado");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _boostDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _reactiveSpeed.Value = _playerSpeed.VerySlow;
            print("No logrado");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _delayDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

    }
}
