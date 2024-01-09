using UnityEngine;
using TMPro;
using UniRx;

namespace Game {

	[RequireComponent(typeof(TextMeshProUGUI))]
    internal sealed class ResourceText : MonoBehaviour {

        [SerializeField] private FloatResourceRx _reactiveResource;

        private TextMeshProUGUI _resource;

        private void Awake() {
            _resource = GetComponent<TextMeshProUGUI>();
            UpdateResourceText(_reactiveResource.Value);
        }

        private void OnEnable() => _reactiveResource.Observable.Subscribe(UpdateResourceText);
        private void OnDisable() => _reactiveResource.Observable.Dispose();

        private void UpdateResourceText(float value) {
            _resource.text = $"{_reactiveResource.Name}: {_reactiveResource.Value:0}";
        }

    }
}
