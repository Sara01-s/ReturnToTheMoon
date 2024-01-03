using UnityEngine;
using TMPro;

namespace Game {

	[RequireComponent(typeof(TextMeshProUGUI))]
    internal sealed class ResourceText : MonoBehaviour {

        [SerializeField] private FloatResourceRx _reactiveResource;

        private TextMeshProUGUI _resource;

        private void Awake() {
            _resource = GetComponent<TextMeshProUGUI>();
        }

       private void Update() {
            UpdateResourceText();
       }

        private void UpdateResourceText() {
            _resource.text = $"{_reactiveResource.Name}: {_reactiveResource.Value:0}";
        }

    }
}
