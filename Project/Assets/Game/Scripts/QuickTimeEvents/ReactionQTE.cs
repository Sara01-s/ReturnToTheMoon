using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    internal sealed class ReactionQTE : MonoBehaviour {
        
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private PlayerSpeed _playerSpeed;

        [SerializeField] private float _preparationTimeInSeconds;

        private const float QTE_SLOWDOWN_DURATION = 2.0f;
        private const float QTE_TIMEWINDOW_DURATION = 3.0f;
        private const float BOOST_DURATION = 1.0f;
        private const float BIG_BOOST_DURATION = 2.0f;

        private float _slowDownTimer;
        private float _timeWindowTimer;
        private float _boostTimer;
        private float _bigBoostDuration;

        private bool _tapped;

        private void Awake() {
            _slowDownTimer = QTE_SLOWDOWN_DURATION;
            _timeWindowTimer = QTE_TIMEWINDOW_DURATION;
            _boostTimer = BOOST_DURATION;
            _bigBoostDuration = BIG_BOOST_DURATION;

            if (_slowDownTimer > 0.0f) {
                _preparationTimeInSeconds *= _slowDownTimer;
            }
        }

        private void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                PlayerInput.OnInput = Tapped;
            }
        }

        private void Tapped() {
            // Otras cosas(?
        }
        
    }
}
