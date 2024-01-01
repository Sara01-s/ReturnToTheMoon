using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using Newtonsoft.Json;

namespace Game {
    
    internal sealed class DistanceCalculator : MonoBehaviour {
        
        [Header("Calculator Data")]
        [SerializeField] private FloatResourceRx _currentDistance;
        [SerializeField] private Transform _start;
        [SerializeField] private Transform _player;
        [Header("HUD Data")]
        [SerializeField] private TextMeshProUGUI _distance;

        private void Awake() {
            _currentDistance.Value = 0.0f;
        }

        private void Update() {
            UpdateCurrentDistance();
            UpdateDistanceText();
        }

        private void UpdateCurrentDistance() {
            _currentDistance.Value = _player.position.x - _start.position.x;
        }

        private void UpdateDistanceText() {
            if (_distance == null) return;
            var distanceText = $"Distance: {_currentDistance.Value.ToString("0")}";
            _distance.text = distanceText;
        }


    }
}
