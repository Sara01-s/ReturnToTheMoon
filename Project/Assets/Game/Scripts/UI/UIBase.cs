using UnityEngine.UIElements;
using UnityEngine;
using System.Collections;

namespace Game {

    internal abstract class UIBase : MonoBehaviour {

        [SerializeField] protected UIDocument _Document;
        [SerializeField] protected StyleSheet _StyleSheet;
        [SerializeField] private bool _updateOnValidate;

        private void OnValidate() {
            if (!Application.isPlaying && gameObject.activeSelf && _updateOnValidate) {
                StartCoroutine(CO_Generate());
            }
        }

        internal void Show() => StartCoroutine(CO_Generate());
        internal void Hide() => Clear();

        protected abstract IEnumerator CO_Generate();
        protected abstract void Clear();

        protected VisualElement Create(params string[] classNames) {
            return Create<VisualElement>(classNames);
        }

        protected T Create<T>(params string[] classNames) where T : VisualElement, new() {
            var element = new T();

            foreach (var className in classNames) {
                element.AddToClassList(className);
            }

            return element;
        }

    }
}
