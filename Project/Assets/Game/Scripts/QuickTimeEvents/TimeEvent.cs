using System.Collections;
using UnityEngine;
using System;

namespace Game {

    internal abstract class TimeEvent : MonoBehaviour {

        [Header("General Event Settings")]
        [SerializeField, Range(0.1f, 2.0f)] protected float _PreparationTimeInSeconds;
        [SerializeField, Range(0.1f, 1.0f)] protected float _SlowMotionFactor;
        [SerializeField, Range(0.1f, 5.0f)] protected float _EventDuration;
        [SerializeField] protected PlayerSpeed _PlayerSpeed;
        
        protected const float DEFAULT_TIMESCALE = 1.0f;
        protected static bool _OnTimeEvent;

        protected abstract IEnumerator CO_InitQTE();
        protected abstract IEnumerator CO_Win();
        protected abstract IEnumerator CO_Lose();
        protected abstract void Input();

        private void Awake() {
            _OnTimeEvent = false;
        }

        protected virtual void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player") && !_OnTimeEvent) {
                _OnTimeEvent = true;
                PlayerInput.OnInput = Input;
                StartCoroutine(CO_InitQTE());
            }
        }

        protected void EndTimeEvent() {
            _OnTimeEvent = false;
            PlayerInput.Reset();
        }

    }
}
