using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    internal sealed class TestCube : MonoBehaviour {

        private float _targetScale = 1.0f;
        Vector3 _scaleSpeed;

        private Quaternion _targetRotation;

        private void OnEnable() {
            TestMenu.OnScaleChanged += OnScaleChanged;
            TestMenu.OnSpinClicked += OnSpinClicked;
        }
        private void OnDisable() {
            TestMenu.OnScaleChanged -= OnScaleChanged;
            TestMenu.OnSpinClicked -= OnSpinClicked;
        }

        private void Update() {
            transform.localScale = Vector3.SmoothDamp(transform.localScale, _targetScale * Vector3.one, ref _scaleSpeed, 0.15f);

            transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, Time.deltaTime * 5.0f);
        }

        private void OnScaleChanged(float newScale) {
            _targetScale = newScale;
        }

        private void OnSpinClicked() {
            _targetRotation = transform.rotation * Quaternion.Euler(Random.insideUnitSphere * 360);
        }

    }
}
