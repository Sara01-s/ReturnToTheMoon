using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UniRx;
using TMPro;

namespace Game {

    internal sealed class VelocityText : MonoBehaviour {
        
        [SerializeField] private FloatResourceRx _reactiveSpeed;
        [SerializeField] private TextMeshProUGUI _speed;

        private void Update() {
            _speed.text = _reactiveSpeed.Value.ToString();
        }

    }
}
