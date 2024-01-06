using UnityEngine;

namespace Game {

	[RequireComponent(typeof(Rigidbody2D), typeof(ConstantForce2D))]
    internal sealed class PlayerMovement : MonoBehaviour {
       
        [SerializeField] private FloatResourceRx _reactiveSpeed;

        [SerializeField] private PlayerSpeed _playerSpeed;
        [SerializeField] private LayerMask _groundLayer;
        [SerializeField] private Transform _leftRaycast;
        [SerializeField] private Transform _midRaycast;
        [SerializeField] private Transform _rightRaycast;
		[SerializeField, Min(0.0f)] private float _midRaycastDst;
		[SerializeField, Min(0.0f)] private float _heightFromFloor;
        
        private const float RAYCAST_DISTANCE = 2.0f;
		private ConstantForce2D _constantForce;

        private void Awake() {
			_constantForce = GetComponent<ConstantForce2D>();
			
            _reactiveSpeed.Value = _playerSpeed.Neutral;
			_constantForce.force = transform.right * _reactiveSpeed.Value;
        }

		private void Update() {
            RotateTowardsFloorNormal();
			StickToTheFloor();
		}

		private void StickToTheFloor() {
			var midHit = Physics2D.Raycast(_midRaycast.position, -transform.up, _midRaycastDst, _groundLayer);
			var floorAnchorPoint = midHit.point.y + _heightFromFloor;

			if (midHit.collider == null) return;

			transform.position = new Vector2(transform.position.x, floorAnchorPoint);
		}

        private void RotateTowardsFloorNormal() {
            var lefthit = Physics2D.Raycast(_leftRaycast.position, -transform.up, RAYCAST_DISTANCE, _groundLayer);
            var righthit = Physics2D.Raycast(_rightRaycast.position, -transform.up, RAYCAST_DISTANCE, _groundLayer);

            if (lefthit.collider == null && righthit.collider == null) return;

			// Inicio: https://www.asteroidbase.com/devlog/7-learning-how-to-walk/
			var avarageNormal = (lefthit.normal + righthit.normal) / 2.0f;

			var targetRotation = Quaternion.FromToRotation(Vector2.up, avarageNormal);
			var finalRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 45.0f);

			transform.rotation = Quaternion.Euler(0.0f, 0.0f, finalRotation.eulerAngles.z);
        }

		
		private void OnDrawGizmos() {
			var midHit = Physics2D.Raycast(_midRaycast.position, -transform.up, _midRaycastDst, _groundLayer);

			Gizmos.color = Color.yellow;
			Gizmos.DrawRay(_midRaycast.position * _midRaycastDst, -transform.up);
			Gizmos.DrawSphere(midHit.point, 0.2f);
		}
       
    }
}
