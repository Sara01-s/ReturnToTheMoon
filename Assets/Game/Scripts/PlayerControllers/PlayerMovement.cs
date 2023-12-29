using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class PlayerMovement : MonoBehaviour {
       
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private FloatResourceRx _reactiveHealth;

        [SerializeField] private PlayerSpeed _startSpeed;
        [SerializeField] private Rigidbody2D _rigidBody;
        [SerializeField] private LayerMask _groundayerMask;
        [SerializeField] private Transform _leftRaycast;
        [SerializeField] private Transform _rightRaycast;
        
        // TEMPORAL
        [SerializeField] private Transform _camera;

        private const float RAYCAST_DISTANCE = 1.0f;
        private float _floatMargin = 0.5f;

        private void Awake() {
            _reactiveSpeed.Value = _startSpeed.Neutral;
        }

        private void LateUpdate() {
            _camera.position = new Vector3(transform.position.x, transform.position.y, _camera.position.z);
        }

        private void FixedUpdate() {
            Move();
        }

        private void Move() {  
            var lefthit = Physics2D.Raycast(_leftRaycast.position, -transform.up, RAYCAST_DISTANCE, _groundayerMask);
            var righthit = Physics2D.Raycast(_rightRaycast.position, -transform.up, RAYCAST_DISTANCE, _groundayerMask);

            if (lefthit.collider != null && righthit.collider != null) {
                // Inicio: https://www.asteroidbase.com/devlog/7-learning-how-to-walk/
                Vector3 avarageNormal = (lefthit.normal + righthit.normal) / 2;
                Vector3 avaragePoint = (lefthit.point + righthit.point) / 2;

                Quaternion targetRotation = Quaternion.FromToRotation(Vector2.up, avarageNormal);
                Quaternion finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 45.0f);
                transform.rotation = Quaternion.Euler(0.0f, 0.0f, finalRotation.eulerAngles.z);

                transform.position = new Vector2(transform.position.x, avaragePoint.y + transform.up.y * 0.5f); // Genera el movimiento erratico en Y
                // Fin.
                _rigidBody.velocity = new Vector2(_reactiveSpeed.Value, _rigidBody.velocity.y) * transform.right;          
            }
        }
       
    }
}
