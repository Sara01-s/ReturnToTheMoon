using UnityEngine;
using UniRx;

namespace Game {

    [CreateAssetMenu(menuName = "Float Resource Rx")]
    internal sealed class FloatResourceRx : ScriptableObject {

		public string Name;
           
        public FloatReactiveProperty Observable => _reactiveProperty;
        public float Value {
            get => _reactiveProperty.Value;
            set => _reactiveProperty.Value = value;
        }

        private readonly FloatReactiveProperty _reactiveProperty = new();

		private void OnDisable() {
			_reactiveProperty.Dispose();
		}

    }
}
