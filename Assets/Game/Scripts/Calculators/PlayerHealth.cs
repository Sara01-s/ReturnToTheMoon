using UnityEngine;
using UniRx;

namespace Game {
    
    internal sealed class PlayerHealth : MonoBehaviour { 
       
        [SerializeField] private FloatResourceRx _playerHealth;
        [SerializeField] internal float MAX_HEALTH = 100.0f;

        private void Awake() {
            _playerHealth.Value = MAX_HEALTH;
        }

        private void OnEnable() {
            _playerHealth.Observable.Subscribe(Die);
        }
        private void OnDisable() {
            _playerHealth.Observable.Dispose();
        }

        private void Die(float health) {
            // TODO: Death Logic, reset level...
        }

    }
}
