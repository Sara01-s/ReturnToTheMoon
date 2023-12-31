using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class Tapping : TimeEvent {

        [Header("Tapping Settings")]
        [SerializeField] private PlayerSpeed _playerSpeed;
        
        [SerializeField]  private int _countGoal;
        [SerializeField]  private float _boostDuration;
        [SerializeField]  private float _delayDuration;

        protected override void Input() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("TapTapTap!");
            
            var startTime = Time.unscaledTimeAsDouble;
            var tapCounter = 0;

            while ((Time.unscaledTimeAsDouble - startTime) <= _eventDuration) {
                var currentTouchPhase = PlayerInput.CurrentTouchPhase;

                if (PlayerInput.CurrentTouchPhase == TouchPhase.Began) {
                    tapCounter++;
                    print("TapCounter: " + tapCounter);
                }
                
                yield return null;
            }

            print("Stop!");
            Time.timeScale = DEFAULT_TIMESCALE;
            if (tapCounter >= _countGoal) StartCoroutine(CO_Win());
            if (tapCounter < _countGoal) StartCoroutine(CO_Lose());
        }

        protected override IEnumerator CO_Win() {
            _playerSpeed.ReactiveResource.Value = _playerSpeed.Fast;
            print("Logrado");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _boostDuration) {
                yield return null;
            }

            PlayerInput.Reset();
            _OnTimeEvent = false;
            _playerSpeed.ReactiveResource.Value = _playerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _playerSpeed.ReactiveResource.Value = _playerSpeed.VerySlow;
            print("No logrado");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _delayDuration) {
                yield return null;
            }

            PlayerInput.Reset();
            _OnTimeEvent = false;
            _playerSpeed.ReactiveResource.Value = _playerSpeed.Neutral;
        }

    }
}
