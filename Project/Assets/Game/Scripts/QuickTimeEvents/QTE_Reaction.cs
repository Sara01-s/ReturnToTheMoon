using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    internal sealed class QTE_Reaction : QuickTimeEvent {
        
        [Header("QTE_Reaction Settings")]
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;

        [SerializeField] private float _boostDuration;
        [SerializeField] private float _bigBoostDuration;
        
        protected override void Input() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("Acierta a tiempo!");
            Time.timeScale = DEFAULT_TIMESCALE; 

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _eventDuration) {
                var currentTouchPhase = PlayerInput.CurrentTouchPhase;
                print("Aprieta!");
                if (currentTouchPhase == TouchPhase.Began) { // Se puede spamear touches y en teoria saldra win casi siempre
                    PlayerInput.Reset();
                    StartCoroutine(CO_Win());
                    yield break;
                }
                
                yield return null;
            }

            PlayerInput.Reset();
            StartCoroutine(CO_Lose());
        }

        protected override IEnumerator CO_Win() {
            _reactiveSpeed.Value = _playerSpeed.VeryFast;
            print("Acertaste!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _bigBoostDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _reactiveSpeed.Value = _playerSpeed.Fast;
            print("Fallaste!");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _boostDuration) {
                yield return null;
            }

            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }
    }
}
