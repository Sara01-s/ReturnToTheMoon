using UnityEngine;
using UniRx;

namespace Game {
    
    internal sealed class DistanceCalculator : MonoBehaviour {
        
        [Header("Calculator Data")]
        [SerializeField] private FloatResourceRx _currentDistance;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _player;

        private void Awake() {
            _currentDistance.Value = 0.0f;
        }

        private void Update() {
            UpdateCurrentDistance();
        }

        private void UpdateCurrentDistance() {
            _currentDistance.Value = _player.position.x - _start.position.x;
        }

    }
}
