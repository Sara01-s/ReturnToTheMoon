using UnityEngine;

namespace Game {

    internal sealed class PlayerMovement : MonoBehaviour {
       
        [SerializeField] private FloatResourceRx _reactiveSpeed;

        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private LayerMask _groundayerMask;
        [SerializeField] private Transform _leftRaycast;
        [SerializeField] private Transform _rightRaycast;
        
        private const float RAYCAST_DISTANCE = 2.0f;

        private void Awake() {
            _reactiveSpeed.Value = _playerSpeed.Neutral;
        }

        private void FixedUpdate() {
            Move();
        }

        private void Move() {
            var lefthit = Physics2D.Raycast(_leftRaycast.position, -transform.up, RAYCAST_DISTANCE, _groundayerMask);
            var righthit = Physics2D.Raycast(_rightRaycast.position, -transform.up, RAYCAST_DISTANCE, _groundayerMask);

            if (lefthit.collider != null && righthit.collider != null) {
                // Inicio: https://www.asteroidbase.com/devlog/7-learning-how-to-walk/
                var avarageNormal = (lefthit.normal + righthit.normal) / 2.0f;
                var avaragePoint = (lefthit.point + righthit.point) / 2.0f;

                var targetRotation = Quaternion.FromToRotation(Vector2.up, avarageNormal);
                var finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 45.0f);


                transform.rotation = Quaternion.Euler(0.0f, 0.0f, finalRotation.eulerAngles.z);
                transform.position = new Vector2(transform.position.x, avaragePoint.y + transform.up.y * 0.75f); // Genera el movimiento erratico en Y
                // Fin.
                _rigidbody.velocity = new Vector2(_reactiveSpeed.Value, _rigidbody.velocity.y) * transform.right;          
            }
        }
       
    }
}
