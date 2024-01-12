using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace Game {

    internal sealed class TestMenu : MonoBehaviour {

        [SerializeField] private UIDocument _document;
        [SerializeField] private StyleSheet _styleSheet;

        internal static event Action<float> OnScaleChanged;
        internal static event Action OnSpinClicked;

        private void Awake() {
            StartCoroutine(CO_Generate());
        }

        private void OnValidate() {
            if (!Application.isPlaying) {
                StartCoroutine(CO_Generate());                
            }
        }

        private IEnumerator CO_Generate() {
            yield return null;
            var root = _document.rootVisualElement;
            root.Clear();

            root.styleSheets.Add(_styleSheet);

            var boxMain = Create("boxMain");
            var viewBox = Create("view-box", "bordered-box");
            var controlBox = Create("control-box", "bordered-box");

            var spinButton = Create<Button>();
            var scaleSlider = Create<Slider>();

            spinButton.text = "Spin";

            scaleSlider.RegisterValueChangedCallback(v => OnScaleChanged?.Invoke(v.newValue));

            spinButton.clicked += OnSpinClicked;

            root.Add(boxMain);
            root.Add(viewBox);
            boxMain.Add(controlBox);
            controlBox.Add(spinButton);
            controlBox.Add(scaleSlider);

            scaleSlider.lowValue = 0.5f;
            scaleSlider.highValue = 2.0f;
            scaleSlider.value = 1.0f;

            root.visible = true;

            if (Application.isPlaying) {
                var targetPosition = boxMain.worldTransform.GetPosition();
                var startPosition = targetPosition + Vector3.up * 100.0f;

                controlBox.experimental.animation.Position(targetPosition, 2000).from = startPosition;
                controlBox.experimental.animation.Start(0.0f, 1.0f, 2000, (e, v) => e.style.opacity = new StyleFloat(v)); // ???
            }

        }

        // WTF
        private VisualElement Create(params string[] classNames) {
            return Create<VisualElement>(classNames);
        }

        // XD
        private T Create<T>(params string[] classNames) where T : VisualElement, new() {
            var element = new T();

            foreach (var className in classNames) {
                element.AddToClassList(className);
            }

            return element;
        }

        private void Clear() {
            _document.rootVisualElement.Clear();
        }


    }
}
