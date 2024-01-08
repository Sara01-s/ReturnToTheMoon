using System.Collections;
using UnityEngine;
using System;

namespace Game {

    internal sealed class Boost : MonoBehaviour {

        [SerializeField] private PlayerStamina _playerStamina;
        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private PlayerLDR _playerLDR;

        [SerializeField] private float _defaultBoostDuration;
        [SerializeField] private float _defaultMaxCoolDown;
        [SerializeField] private float _defaultBostCost;

        internal event Action OnStart;
		internal event Action OnCooldown;

        private bool _boosted;

        private void OnEnable() => PlayerInput.SetDefaultInputCallback(BoostSpeed);
        private void OnDisable() => PlayerInput.OnInput = null;

        private void BoostSpeed() {
            if (!_boosted && PlayerInput.CurrentTouchPhase == TouchPhase.Began) {
                StartCoroutine(CO_Boosted());
            }
        }

        private IEnumerator CO_Boosted() {
            OnStart?.Invoke();
            
			_boosted = true;

            _playerStamina.ReactiveResource.Value -= _defaultBostCost;
            _playerLDR.ReactiveResource.Value = _playerLDR.MinDefLDR;
            _playerSpeed.ReactiveResource.Value = _playerSpeed.Fast;

            Debug.Log("Boost activated, cooldown started...");

			float elapsedTime = 0.0f;

			while (elapsedTime < _defaultBoostDuration) {
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			
            Debug.Log("Boost ended, waiting for cooldown to end.");
            
            _playerSpeed.ReactiveResource.Value = _playerSpeed.Neutral;
            _playerLDR.ReactiveResource.Value = _playerLDR.DefaultLDR;

			StartCoroutine(CO_StartCooldown(_defaultMaxCoolDown));
        }

        private IEnumerator CO_StartCooldown(double cooldownDuration) {
            OnCooldown?.Invoke();
            
            float elapsedTime = 0.0f;

            while (elapsedTime < cooldownDuration) {
                elapsedTime += Time.deltaTime;
                yield return null;
            }
			
			Debug.Log("Cooldown reseted.");
            _boosted = false;
        }

    }
}
