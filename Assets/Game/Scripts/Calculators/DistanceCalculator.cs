using UnityEngine;

namespace Game {
    
    internal sealed class DistanceCalculator : MonoBehaviour {
        
        [SerializeField] private FloatResourceRx _currentDistance;
        
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _player;

        private void Update() {
            UpdateCurrentDistance();
        }

        private void UpdateCurrentDistance() {
            _currentDistance.Value = _player.position.x - _start.position.x;
            print(_currentDistance.Value);
        }


    }
}
