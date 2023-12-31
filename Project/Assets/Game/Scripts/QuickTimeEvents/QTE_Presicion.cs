using System.Collections;
using UnityEngine;
using System;

namespace Game {

    internal sealed class QTE_Presicion : QuickTimeEvent {

        [Header("QTE_Presicion Settings")]
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;

        [SerializeField, Range(0.1f, 5.0f)] private float _oscillatonSpeed;
        [SerializeField, Range(0.0f, 1.0f)] private float _minTargetRange, _maxTargetRange;
        [SerializeField, Range(0.0f, 1.0f)] private float _minRange, _maxRange;

        [SerializeField]  private float _boostDuration;
        [SerializeField]  private float _delayDuration;

        private float _oscillationValue;

        protected override void Input() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);

            print("Acierta correctamente!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _eventDuration) {
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

            PlayerInput.Reset();
            Time.timeScale = DEFAULT_TIMESCALE;  
            StartCoroutine(CO_Lose()); 
        }

        protected override IEnumerator CO_Win() {
            _reactiveSpeed.Value = _playerSpeed.Fast;
            print("Lo lograste!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _boostDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _reactiveSpeed.Value = _playerSpeed.VerySlow;
            print("Fallaste");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _delayDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }        

    }
}
