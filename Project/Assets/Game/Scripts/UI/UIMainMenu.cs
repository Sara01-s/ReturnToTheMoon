using UnityEngine.UIElements;
using System.Collections;
using UnityEngine;

namespace Game {

    internal sealed class UIMainMenu : UIBase
    {

        private void Awake() {
            StartCoroutine(CO_Generate());
        }

        protected override IEnumerator CO_Generate() {
            yield return null;
            var root = _Document.rootVisualElement;
            root.Clear();

            root.styleSheets.Add(_StyleSheet);

            var titleBox = Create<VisualElement>("titleBox");
            root.Add(titleBox);

            var titleText = Create<TextElement>("titleText");
            titleText.text = "Return to the Moon";
            titleBox.Add(titleText);

            var optionsBox = Create<VisualElement>("optionsBox");
            root.Add(optionsBox);

            var playButton = Create<Button>("playButton");
            var settingsButton = Create<Button>("button");
            var quitButton = Create<Button>("button");
            
            playButton.text = "Play";
            settingsButton.text = "Settings";
            quitButton.text = "Quit";

            optionsBox.Add(playButton);
            optionsBox.Add(settingsButton);
            optionsBox.Add(quitButton);

            var versionBox = Create<VisualElement>("versionBox");
            root.Add(versionBox);

            var versionText = Create<TextElement>("versionText");
            versionText.text = "v0.1.0";
            versionBox.Add(versionText);

            root.visible = true;
        }

        protected override void Clear() {
            _Document.rootVisualElement.Clear();
        }
    }
}
