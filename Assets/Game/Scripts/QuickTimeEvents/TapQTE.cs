using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class TapQTE : MonoBehaviour {

        [SerializeField] private FloatResourceRx _reactiveSpeed;

        [SerializeField] private PlayerSpeed _playerSpeed;

        [SerializeField]  private float HAZARD_QTE_SLOWDOWN_DURATION;
        [SerializeField]  private float HAZARD_QTE_BOOST_DURATION;
        [SerializeField]  private float QTE_SLOWDOWN_DURATION;
        [SerializeField]  private float QTE_SPEED_INPUTCOUNT;

        private float _preparationTimeInSeconds;
        private bool _tapped;

        private float _hazardBoostTimer;
        private float _hazardSlowDownTimer;
        private float _slowDownTimer;

        private void Awake() {
            _hazardSlowDownTimer = HAZARD_QTE_SLOWDOWN_DURATION;
            _hazardBoostTimer = HAZARD_QTE_BOOST_DURATION;
            _slowDownTimer = QTE_SLOWDOWN_DURATION;
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag("Player")) {
                print("Iniciado");
                PlayerInput.OnInput = Tapped;
                StartCoroutine(CO_INITQTE());
            }
        }

        private void Tapped(Touch touch) {
            if (touch.phase != TouchPhase.Began) return;
            _tapped = true;
        }

        private IEnumerator CO_INITQTE() {
            yield return new WaitForSeconds(_preparationTimeInSeconds);

            var tapCounter = 0;

            while (_slowDownTimer > 0.0f) {
                _slowDownTimer -= Time.deltaTime;
                if (_tapped) {
                    tapCounter++;
                }
                _tapped = false;
                print(tapCounter);
            }

            _slowDownTimer = QTE_SLOWDOWN_DURATION;
            if (tapCounter >= QTE_SPEED_INPUTCOUNT) StartCoroutine(CO_BOOST());
            if (tapCounter <= QTE_SPEED_INPUTCOUNT) StartCoroutine(CO_DECREASE());
        }

        private IEnumerator CO_BOOST() {
            _reactiveSpeed.Value = _playerSpeed.Fast;

            while (_hazardBoostTimer > 0.0f) {
                _hazardBoostTimer -= Time.deltaTime;
                yield return null;
            }

            _hazardBoostTimer = HAZARD_QTE_BOOST_DURATION;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        private IEnumerator CO_DECREASE() {
            _reactiveSpeed.Value = _playerSpeed.VerySlow;

            while (_hazardSlowDownTimer > 0.0f) {
                _hazardSlowDownTimer -= Time.deltaTime;
                yield return null;
            }

            _hazardSlowDownTimer = HAZARD_QTE_SLOWDOWN_DURATION;
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

    }
}