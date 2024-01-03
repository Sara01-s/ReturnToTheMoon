using System.Collections;
using UnityEngine;
using System;

namespace Game {

    internal sealed class Presicion : TimeEvent {

        [Header("Presicion Settings")]
        [SerializeField] private float _boostTime;
        [SerializeField] private float _delayTime;
        [SerializeField, Range(0.1f, 5.0f)] private float _oscillatonSpeed;
        [SerializeField, Range(0.0f, 1.0f)] private float _minTargetRange, _maxTargetRange;
        [SerializeField, Range(0.0f, 1.0f)] private float _minRange, _maxRange;

        private float _oscillationValue;

        protected override void OnInput() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("Acierta correctamente!");

            double startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _EventDuration) {
                var oscillationTime = Time.unscaledTime * _oscillatonSpeed;
                _oscillationValue = Mathf.PingPong(oscillationTime, 1.0f);

                var currentTouchPhase = PlayerInput.CurrentTouchPhase;

                if (VVT.Common.IsBetween(_oscillationValue, _minTargetRange, _maxTargetRange)) print("ahora");

                if (currentTouchPhase == TouchPhase.Began) {
                    if (VVT.Common.IsBetween(_oscillationValue, _minTargetRange, _maxTargetRange)) {
                        PlayerInput.Reset();
                        Time.timeScale = DEFAULT_TIMESCALE; 
                        StartCoroutine(CO_Win());
                        yield break;
                    }
                    else {
                        PlayerInput.Reset();
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

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _boostTime) {
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.VerySlow;
            print("Fallaste");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _delayTime) {
                yield return null;
            }

            EndTimeEvent();
            _PlayerSpeed.ReactiveResource.Value = _PlayerSpeed.Neutral;
        }        

    }
}
