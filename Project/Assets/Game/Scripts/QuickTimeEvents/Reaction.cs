using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class Reaction : TimeEvent {

        [Header("Reaction Settings")]
        [SerializeField] private float _boostTime;
        [SerializeField] private float _bigBoostTime;

        protected override void OnInput() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
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
                print("Aprieta!");
                if (currentTouchPhase == TouchPhase.Began) { // Se puede spamear touches y en teoria saldra win casi siempre
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

            float elapsedTime = 0.0f;

            while (elapsedTime < _bigBoostTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Fast;
            print("Fallaste!");

            float elapsedTime = 0.0f;

            while (elapsedTime < _boostTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }
    }
}
