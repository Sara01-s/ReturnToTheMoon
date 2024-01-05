using System.Collections;
using UnityEngine;
using System;

namespace Game {

    internal sealed class Tapping : TimeEvent {

        [Header("Tapping Settings")]
        [SerializeField] private int _numTapsGoal;
        [SerializeField] private float _boostTime;
        [SerializeField] private float _delayTime;

		internal event Action<float> OnPreparation;
		internal event Action OnStart;
		internal event Action OnSuccess;
		internal event Action OnFail;

        protected override void OnInput() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;
			OnPreparation?.Invoke(_PreparationTimeInSeconds);
            print("Preparing Tap QTE...");

            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            
			OnStart?.Invoke();

			float elapsedTime = 0.0f;
            int numTaps = 0;

            while (elapsedTime < _EventDuration) {

				print("Tap!Tap!");

                if (PlayerInput.CurrentTouchPhase == TouchPhase.Began) {
                    numTaps++;
                    print("TapCounter: " + numTaps);
                }
                
				elapsedTime += Time.unscaledDeltaTime;
				print(elapsedTime);
                yield return null;
            }

            Time.timeScale = DEFAULT_TIMESCALE;
            print("Tiempo de Tap terminado!");

            if (numTaps >= _numTapsGoal) {
				StartCoroutine(CO_Win());
			}
			
            if (numTaps < _numTapsGoal) {
				StartCoroutine(CO_Lose());
			}
        }

        protected override IEnumerator CO_Win() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Fast;
            print("Tap completado");

			yield return new WaitForSeconds(_boostTime);

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.VerySlow;
            print("Tap fallado");

			yield return new WaitForSeconds(_boostTime);

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

    }
}
