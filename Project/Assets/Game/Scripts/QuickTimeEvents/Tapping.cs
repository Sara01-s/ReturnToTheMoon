using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class Tapping : TimeEvent {

        [Header("Tapping Settings")]
        [SerializeField] private int _countGoal;
        [SerializeField] private float _boostTime;
        [SerializeField] private float _delayTime;

        protected override void OnInput() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("TapTapTap!");
            
            float elapsedTime = 0.0f;
            int numTaps = 0;

            while (elapsedTime < _EventDuration) {
                elapsedTime += Time.unscaledDeltaTime;

                if (PlayerInput.CurrentTouchPhase == TouchPhase.Began) {
                    numTaps++;
                    print("TapCounter: " + numTaps);
                }
                
                yield return null;
            }

            Time.timeScale = DEFAULT_TIMESCALE;
            print("Detente!");

            if (numTaps >= _countGoal) {
				StartCoroutine(CO_Win());
			}
			
            if (numTaps < _countGoal) {
				StartCoroutine(CO_Lose());
			}
        }

        protected override IEnumerator CO_Win() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Fast;
            print("Logrado");

            float elapsedTime = 0.0f;

            while (elapsedTime < _boostTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.VerySlow;
            print("No logrado");

            float elapsedTime = 0.0f;

            while (elapsedTime < _delayTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

    }
}
