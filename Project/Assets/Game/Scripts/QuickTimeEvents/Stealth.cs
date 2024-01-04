using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class Stealth : TimeEvent {

        [Header("Stealth Settings")]
        [SerializeField] private float _delayTime;
        [SerializeField] private float _bigDelayTime;
        
        private readonly TouchPhase _stationary = TouchPhase.Stationary; 
        private readonly TouchPhase _moved = TouchPhase.Moved; 

        protected override void OnInput() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Stationary) return;
            // Otros eventos(?
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            Time.timeScale = DEFAULT_TIMESCALE;

            float elapsedTime = 0.0f;

            while (elapsedTime < _EventDuration) {
                elapsedTime += Time.deltaTime;

                var currentTouchPhase = PlayerInput.CurrentTouchPhase;

                if (currentTouchPhase != _stationary && currentTouchPhase != _moved) {
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

            float elapsedTime = 0.0f;

            while (elapsedTime < _delayTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.VerySlow;
            print("Bad!");

            float elapsedTime = 0.0f;

            while (elapsedTime < _bigDelayTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

    }
}
