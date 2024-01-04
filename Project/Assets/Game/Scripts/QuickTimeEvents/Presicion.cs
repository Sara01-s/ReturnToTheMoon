using System.Collections;
using NaughtyAttributes;
using UnityEngine;
using System;

namespace Game {

    internal sealed class Presicion : TimeEvent {

        [Header("Presicion Settings")]
        [SerializeField] private float _boostTime;
        [SerializeField] private float _delayTime;
        [SerializeField, Range(0.1f, 5.0f)] private float _oscillatonSpeed;
        [SerializeField, MinMaxSlider(0, 1)] private Vector2 _targetRange;

        private float _oscillationValue;

        protected override void OnInput() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("Acierta correctamente!");

            float elapsedTime = 0.0f;

            while (elapsedTime < _EventDuration) {
                elapsedTime += Time.unscaledDeltaTime;

                var oscillationTime = Time.unscaledTime * _oscillatonSpeed;
                _oscillationValue = Mathf.PingPong(oscillationTime, 1.0f);

                if (VVT.Common.IsBetween(_oscillationValue, _targetRange.x, _targetRange.y)) print("ahora");
                
                var currentTouchPhase = PlayerInput.CurrentTouchPhase;

                if (currentTouchPhase == TouchPhase.Began) {
                    if (VVT.Common.IsBetween(_oscillationValue, _targetRange.x, _targetRange.y)) {
                        Time.timeScale = DEFAULT_TIMESCALE; 
                        StartCoroutine(CO_Win());
                        yield break;
                    }
                    else {
                        Time.timeScale = DEFAULT_TIMESCALE; 
                        StartCoroutine(CO_Lose());
                        yield break;
                    }
                }

                yield return null;
            }

            Time.timeScale = DEFAULT_TIMESCALE;  
            StartCoroutine(CO_Lose()); 
        }

        protected override IEnumerator CO_Win() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Fast;
            print("Lo lograste!");

            var elapsedTime = 0.0f;

            while (elapsedTime < _boostTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.VerySlow;
            print("Fallaste");

            var elapsedTime = 0.0f;

            while (elapsedTime < _delayTime) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }        

    }
}
