using System;
using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class QTE_Tapping : QuickTimeEvent {

        [Header("Quick Time Event Settings")]
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;
        
        [SerializeField]  private int _countGoal;
        [SerializeField]  private float _coolDownDuration;
        [SerializeField]  private float _boostDuration;

        private float _hazardBoostTimer;
        private float _hazardSlowDownTimer;

        private bool _tapped;

        private void Awake() {
            _hazardSlowDownTimer = _coolDownDuration;
            _hazardBoostTimer = _boostDuration;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                PlayerInput.OnInput = Tapped;

                StartCoroutine(CO_InitQTE());
            }
        }

        private void Tapped() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
            _tapped = true;
            // Otras cosas(?
        }

        protected override IEnumerator CO_InitQTE() {
            Time.timeScale = _SlowMotionFactor;            
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSecondsRealtime(_PreparationTimeInSeconds);
            print("TapTapTap!");
            
            var startTime = Time.unscaledTimeAsDouble;
            var tapCounter = 0;

            while ((Time.unscaledTimeAsDouble - startTime) <= _SlowDownDuration) {
                
                if (_tapped) {
                    tapCounter++;
                    _tapped = false;
                    print("TapCounter: " + tapCounter);
                }
                
                yield return null;
            }

            Time.timeScale = DEFAULT_TIMESCALE;
            print("Stop!");
            PlayerInput.Reset();
            if (tapCounter >= _countGoal) StartCoroutine(CO_Win());
            if (tapCounter < _countGoal) StartCoroutine(CO_Lose());
        }

        protected override IEnumerator CO_Win() {
            _reactiveSpeed.Value = _playerSpeed.Fast;
            print("Logrado");

            while (_hazardBoostTimer > 0.0f) {
                _hazardBoostTimer -= Time.deltaTime;
                yield return null;
            }

            _hazardBoostTimer = _boostDuration;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        protected override IEnumerator CO_Lose() {
            _reactiveSpeed.Value = _playerSpeed.VerySlow;
            print("No logrado");

            while (_hazardSlowDownTimer > 0.0f) {
                _hazardSlowDownTimer -= Time.deltaTime;
                yield return null;
            }

            _hazardSlowDownTimer = _coolDownDuration;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

    }
}
