using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class Stealth : TimeEvent {

        [Header("Stealth Settings")]
        [SerializeField] private float _delayTime;
        [SerializeField] private float _bigDelayTime;
        
        private TouchPhase _stationary = TouchPhase.Stationary; 
        private TouchPhase _moved = TouchPhase.Moved; 

        protected override void Input() {
            // Otros eventos(?
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            Time.timeScale = DEFAULT_TIMESCALE;

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _EventDuration) {
                var currentTouchPhase = PlayerInput.CurrentTouchPhase;

                if (currentTouchPhase != _stationary && currentTouchPhase != _moved) {
                    PlayerInput.Reset();
                    StartCoroutine(CO_Lose());
                    yield break;
                }

                print("Hold!");
                yield return null;
            }

            StartCoroutine(CO_Win());
        }

        protected override IEnumerator CO_Win() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Slow;
            print("Well Done!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _delayTime) {
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.VerySlow;
            print("Bad!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _bigDelayTime) {
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

    }
}
