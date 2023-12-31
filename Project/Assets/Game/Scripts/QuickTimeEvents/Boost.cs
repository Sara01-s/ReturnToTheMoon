using System.Collections;
using NSubstitute;
using UnityEngine;

namespace Game {

    internal sealed class Boost : MonoBehaviour {

        [SerializeField] private PlayerStamina _playerStamina;
        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private PlayerLDR _playerLDR;

        [SerializeField] private float _defaultBoostDuration;
        [SerializeField] private float _defaultMaxCoolDown;
        [SerializeField] private float _defaultBostCost;

        private bool _boosted;

        private void OnEnable() {
            PlayerInput.SetDefaultInput(Boosted);
        }
        private void OnDisable() {
            PlayerInput.OnInput = null;
        }

        private void Boosted() {
            if (_boosted || PlayerInput.CurrentTouchPhase != TouchPhase.Began) return;
            _boosted = true;
            StartCoroutine(CO_Boosted());
        }

        private IEnumerator CO_Boosted() {
            _playerStamina.ReactiveResource.Value -= _defaultBostCost;
            _playerLDR.ReactiveResource.Value = _playerLDR.MinDefLDR;
            _playerSpeed.ReactiveResource.Value = _playerSpeed.Fast;

            Debug.Log("Boost started");

            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _defaultBoostDuration) {
                yield return null;
            }

            Debug.Log("Boost ended");
            
            _playerSpeed.ReactiveResource.Value = _playerSpeed.Neutral;
            _playerLDR.ReactiveResource.Value = _playerLDR.DefaultLDR;

            StartCoroutine(CO_COOLDOWN());
        }

        private IEnumerator CO_COOLDOWN() {
            var startTime = Time.unscaledTimeAsDouble;

            while ((Time.unscaledTimeAsDouble - startTime) <= _defaultMaxCoolDown) {
                yield return null;
            }
            _boosted = false;
        }

    }
}
