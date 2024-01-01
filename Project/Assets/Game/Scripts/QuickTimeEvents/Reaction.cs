using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class Reaction : TimeEvent {

        [Header("Reaction Settings")]
        [SerializeField] private float _boostTime;
        [SerializeField] private float _bigBoostTime;
        
        protected override void Input() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            Time.timeScale = DEFAULT_TIMESCALE; 

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _EventDuration) {
                var currentTouchPhase = PlayerInput.CurrentTouchPhase;
                print("Aprieta!");
                if (currentTouchPhase == TouchPhase.Began) { // Se puede spamear touches y en teoria saldra win casi siempre
                    PlayerInput.Reset();
                    StartCoroutine(CO_Win());
                    yield break;
                }
                
                yield return null;
            }

            StartCoroutine(CO_Lose());
        }

        protected override IEnumerator CO_Win() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.VeryFast;
            print("Acertaste!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _bigBoostTime) {
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Fast;
            print("Fallaste!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _boostTime) {
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }
    }
}
