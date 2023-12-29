using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class PlayerMovement : MonoBehaviour {
       
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private FloatResourceRx _reactiveHealth;

        [SerializeField] private PlayerSpeed _startSpeed;
        [SerializeField] private Rigidbody2D _rigidBody;

        [SerializeField] private bool _moveOnStart;

        private void Awake() {
            _reactiveSpeed.Value = _startSpeed.Neutral;
        }

        private void Start() {
            if (_moveOnStart) {
                StartCoroutine(CO_MOVE());
            }
        }

        private void Move() {
            _rigidBody.AddForce(transform.right * _reactiveSpeed.Value * Time.deltaTime, ForceMode2D.Impulse);
        }

        private IEnumerator CO_MOVE() {
            while (_reactiveHealth.Value > 0.0f) {
                Move();
                yield return null;
            }
        }
       
    }
}
