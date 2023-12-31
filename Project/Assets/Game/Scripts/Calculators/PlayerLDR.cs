using UnityEngine;
using UniRx;

namespace Game {

    internal sealed class PlayerLDR : PlayerResource {
        
        
        [SerializeField] internal float MinLDR;
        [SerializeField] internal float MinDefLDR;
        [SerializeField] internal float DefaultLDR;
        [SerializeField] internal float MaxDefLDR;
        [SerializeField] internal float MaxLDR;

        [SerializeField] private FloatResourceRx _playerHealth;

        private void Awake() {
            ReactiveResource.Value = DefaultLDR;
        }

        private void Update() {
            DecrementLife();
        }

        private void DecrementLife() {
            _playerHealth.Value -= Mathf.Abs(ReactiveResource.Value) * Time.deltaTime;
        }

    }
}
