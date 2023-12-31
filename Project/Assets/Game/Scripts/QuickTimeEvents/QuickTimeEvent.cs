using System.Collections;
using UnityEngine;
using System;

namespace Game {

    internal abstract class QuickTimeEvent : MonoBehaviour {
        
        [Header("General Event Settings")]
        [SerializeField, Range(0.1f, 2.0f)] protected float _PreparationTimeInSeconds;
        [SerializeField, Range(0.1f, 1.0f)] protected float _SlowMotionFactor;
        [SerializeField, Range(0.1f, 5.0f)] protected float _eventDuration;
        
        protected const float DEFAULT_TIMESCALE = 1.0f;

        internal void Initialize() => StartCoroutine(CO_InitQTE());
        protected abstract IEnumerator CO_InitQTE();
        protected abstract IEnumerator CO_Win();
        protected abstract IEnumerator CO_Lose();

    }
}
