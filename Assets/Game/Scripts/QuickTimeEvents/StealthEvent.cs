using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    internal sealed class StealthEvent : MonoBehaviour {
        
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private float _preparationTimeInSeconds;
        [SerializeField] private float _timeSlow;

        private const float STEALTH_EVENT_DURATION = 5.0f;
        private const float DELAY_DURATION = 1.5f;
        private const float BIG_DELAY_DURATION = 3.0f;
        private const float DEFAULT_TIME_SCALE = 1.0f;

        private float _stealthEventTimer;
        private float _delayTimer;
        private float _bigDelayTimer;

        private void Awake() {
            _stealthEventTimer = STEALTH_EVENT_DURATION;
            _delayTimer = DELAY_DURATION;
            _bigDelayTimer = BIG_DELAY_DURATION;

            if (_timeSlow > 0.0f) {
                _preparationTimeInSeconds *= _timeSlow;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                PlayerInput.OnInput = Pressed;

                StartCoroutine(CO_INITQTE());
            }
        }

        private void Pressed() {
            // Otros eventos(?
        }

        private IEnumerator CO_INITQTE() {
            Time.timeScale = _timeSlow;
            print("Esperando el tiempo de preparacion...");
            yield return new WaitForSeconds(_preparationTimeInSeconds);
            print("Terminado el tiempo de preparacion...");
            Time.timeScale = DEFAULT_TIME_SCALE;

            while (_stealthEventTimer > 0.0f) {
                _stealthEventTimer -= Time.deltaTime;

                if (PlayerInput.CurrentTouchPhase != TouchPhase.Stationary && PlayerInput.CurrentTouchPhase != TouchPhase.Moved) {
                    StartCoroutine(CO_DECREASE());
                    yield break;
                }

                print("Hold!");

                yield return null;
            }

            PlayerInput.Reset();
            _stealthEventTimer = STEALTH_EVENT_DURATION;
            StartCoroutine(CO_BOOST());
        }

        private IEnumerator CO_BOOST() {
            _reactiveSpeed.Value = _playerSpeed.Slow;
            print("Well Done!");
            while (_delayTimer > 0.0f) {
                _delayTimer -= Time.deltaTime;
                yield return null;
            }

            _delayTimer = DELAY_DURATION;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        private IEnumerator CO_DECREASE() {
            _reactiveSpeed.Value = _playerSpeed.VerySlow;
            print("Bad!");
            while (_bigDelayTimer > 0.0f) {
                _bigDelayTimer -= Time.deltaTime;
                yield return null;
            }

            _bigDelayTimer = BIG_DELAY_DURATION;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

    }
}
