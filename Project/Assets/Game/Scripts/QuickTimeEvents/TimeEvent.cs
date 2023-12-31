using System.Collections;
using UnityEngine;
using System;
using Mono.Cecil;

namespace Game {

    internal abstract class TimeEvent : MonoBehaviour {

        [Header("General Event Settings")]
        [SerializeField, Range(0.1f, 2.0f)] protected float _PreparationTimeInSeconds;
        [SerializeField, Range(0.1f, 1.0f)] protected float _SlowMotionFactor;
        [SerializeField, Range(0.1f, 5.0f)] protected float _eventDuration;
        
        protected const float DEFAULT_TIMESCALE = 1.0f;

        protected abstract IEnumerator CO_InitQTE();
        protected abstract IEnumerator CO_Win();
        protected abstract IEnumerator CO_Lose();
        protected abstract void Input();

        protected virtual void OnTriggerEnter2D(Collider2D collider) {
            if (collider.CompareTag("Player")) {
                PlayerInput.OnInput = Input;
                StartCoroutine(CO_InitQTE());
            }
        }

    }
}
