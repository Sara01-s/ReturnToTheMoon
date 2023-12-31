using UnityEngine;

namespace Game {

    internal sealed class Play : MonoBehaviour {

        [SerializeField] private Rigidbody2D _playerRigidbody;

        private void Awake() {
            _playerRigidbody.simulated = false;
        }

        public void EnableSimulation() {
            _playerRigidbody.simulated = true;
        }

    }
}
