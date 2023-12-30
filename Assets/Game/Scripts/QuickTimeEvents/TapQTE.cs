using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class TapQTE : MonoBehaviour {

        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;
        
        [SerializeField] private float _timeSlow;

        [SerializeField]  private float QTE_SPEED_INPUTCOUNT;
        [SerializeField]  private float QTE_SLOWDOWN_DURATION;
        [SerializeField]  private float HAZARD_QTE_SLOWDOWN_DURATION;
        [SerializeField]  private float HAZARD_QTE_BOOST_DURATION;

        private const float DEFAULT_TIME_SCALE = 1.0f;

        private float _hazardBoostTimer;
        private float _hazardSlowDownTimer;
        private float _slowDownTimer;

        private bool _tapped;
        
        private void Awake() {
            _hazardSlowDownTimer = HAZARD_QTE_SLOWDOWN_DURATION;
            _hazardBoostTimer = HAZARD_QTE_BOOST_DURATION;
            _slowDownTimer = QTE_SLOWDOWN_DURATION;

            if (_timeSlow > 0.0f) {
                _slowDownTimer *= _timeSlow; // Normalizar el tiempo debido al cambio en el timeScale.
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                PlayerInput.OnInput = Tapped;

                StartCoroutine(CO_INITQTE());
            }
        }

        private void Tapped() {
            if (PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
            _tapped = true;
            // Otras cosas(?
        }

        private IEnumerator CO_INITQTE() {
            Time.timeScale = _timeSlow;
            print("TapTapTap!");
            
            var tapCounter = 0;

            while (_slowDownTimer > 0.0f) {
                _slowDownTimer -= Time.deltaTime;
                
                if (_tapped) {
                    tapCounter++;
                    print("TapCounter: " + tapCounter);
                    _tapped = false;
                }
                
                yield return null;
            }

            Time.timeScale = DEFAULT_TIME_SCALE;
            print("Stop!");

            PlayerInput.Reset();
            _slowDownTimer = QTE_SLOWDOWN_DURATION;
            if (tapCounter >= QTE_SPEED_INPUTCOUNT) StartCoroutine(CO_BOOST());
            if (tapCounter < QTE_SPEED_INPUTCOUNT) StartCoroutine(CO_DECREASE());
        }

        private IEnumerator CO_BOOST() {
            _reactiveSpeed.Value = _playerSpeed.Fast;
            print("Logrado");

            while (_hazardBoostTimer > 0.0f) {
                _hazardBoostTimer -= Time.deltaTime;
                yield return null;
            }

            _hazardBoostTimer = HAZARD_QTE_BOOST_DURATION;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        private IEnumerator CO_DECREASE() {
            _reactiveSpeed.Value = _playerSpeed.VerySlow;
            print("No logrado");

            while (_hazardSlowDownTimer > 0.0f) {
                _hazardSlowDownTimer -= Time.deltaTime;
                yield return null;
            }

            _hazardSlowDownTimer = HAZARD_QTE_SLOWDOWN_DURATION;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

    }
}
