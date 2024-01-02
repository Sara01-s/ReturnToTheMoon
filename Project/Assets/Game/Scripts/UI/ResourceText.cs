using UnityEngine;
using TMPro;
using UniRx;

namespace Game {

    internal sealed class ResourceText : MonoBehaviour {

        [SerializeField] private FloatResourceRx _reactiveResource;
        [SerializeField] private string _resourceType;

        private TextMeshProUGUI _resource;

        private void Awake() {
            _resource = GetComponent<TextMeshProUGUI>();
        }

       private void Update() {
            UpdateResourceText();
       }

        private void UpdateResourceText() {
            var currentText = $"{_resourceType}: {_reactiveResource.Value.ToString("0")}";
            _resource.text = currentText;
        }

    }
}
