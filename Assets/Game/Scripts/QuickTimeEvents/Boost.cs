using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class Boost : MonoBehaviour {

        [SerializeField] private FloatResourceRx _reactiveStamina;
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private FloatResourceRx _reactiveLDR;

        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private PlayerLDR _playerLDR;

        [SerializeField] private float DEFAULT_BOOST_MAX_COOLDOWN;
        [SerializeField] private float DEFAULT_BOOST_DURATION;
        [SerializeField] private float DEFAULT_BOOST_COST;

        private float _boostDurationTimer;
        private float _coolDownTimer;

        private bool _boosted;

        private void Awake() {
            _boostDurationTimer = DEFAULT_BOOST_DURATION;
            _coolDownTimer = DEFAULT_BOOST_MAX_COOLDOWN;
        }
    
        private void OnEnable() {
            PlayerInput.SetDefaultInput(Boosted);
        }
        private void OnDisable() {
            PlayerInput.OnInput = null;
        }

        private void Boosted(Touch touch) {
            if (_boosted || touch.phase != TouchPhase.Began) return;
            _boosted = true;
            StartCoroutine(CO_BOOSTED());
        }

        private IEnumerator CO_BOOSTED() {
            _reactiveSpeed.Value = _playerSpeed.Fast;
            _reactiveStamina.Value -= DEFAULT_BOOST_COST;
            _reactiveLDR.Value = _playerLDR.BETWEEN_MIN_DEFAULT_LDR;

            Debug.Log("Boosted");

            while (_boostDurationTimer > 0.0f) {
                _boostDurationTimer -= Time.deltaTime;
                yield return null;
            }
            
            _reactiveSpeed.Value = _playerSpeed.Neutral;
            _reactiveLDR.Value = _playerLDR.DEFAULT_LDR;
            _boostDurationTimer = DEFAULT_BOOST_DURATION;

            StartCoroutine(CO_COOLDOWN());
        }

        private IEnumerator CO_COOLDOWN() {
            while (_coolDownTimer > 0.0f) {
                _coolDownTimer -= Time.deltaTime;
                yield return null;
            }
            _coolDownTimer = DEFAULT_BOOST_MAX_COOLDOWN;
            _boosted = false;
        }

    }
}
