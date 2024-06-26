using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine;
using VVT.Data;

namespace VVT.Runtime {
    
    internal sealed class SceneController : MonoBehaviour, ISceneService {

        [SerializeField] private Context _loadingContext;

        private ISettingsDataService _settingsDataService;
        private IGameDataService _gameDataService;
        private IContextService _contextService;
        
        private void Awake() {
            Services.Instance.RegisterService<ISceneService>(this);
            
            _gameDataService = Services.Instance.GetService<IGameDataService>();
            _settingsDataService = Services.Instance.GetService<ISettingsDataService>();
        }

		private void Start() {
            _contextService = Services.Instance.GetService<IContextService>();
		}

        private void OnDisable() {
            Services.Instance.UnRegisterService<ISceneService>();
        }

        public void LoadScene(int sceneBuildIndex) {

			int scenes = SceneManager.sceneCountInBuildSettings;
            
            if (!Common.IsBetween(sceneBuildIndex, -1, scenes)) {
                Logs.LogError($"Can't load scene with build index {sceneBuildIndex}, There are {scenes} scenes in Build Settings.", ErrorCode.BadArgument);
                return;
            }

            StopAllCoroutines();
            StartCoroutine(CO_LoadScene(sceneBuildIndex));
        }

		
        public void LoadSceneImmediate(int sceneBuildIndex) {
            SceneManager.LoadScene(sceneBuildIndex);
        }

		public void LoadPreviousSceneImmediate() {
			int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
            SceneManager.LoadScene(previousSceneIndex);
        }

		public void LoadNextSceneImmediate() {
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }

		public void LoadPreviousScene() {
			int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1;
			LoadScene(previousSceneIndex);
		}

		public void LoadNextScene() {
			int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
			LoadScene(nextSceneIndex);
		}

        public void ReloadScene() {
            if (_contextService.ContextsInfo.GamePaused) _contextService.ToggleGamePause();
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

		public void ReloadSceneImmediate() {
            if (_contextService.ContextsInfo.GamePaused) _contextService.ToggleGamePause();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

		private IEnumerator CO_LoadScene(int sceneBuildIndex) {

            Time.timeScale = 1.0f;

            _gameDataService.SaveData();
            _settingsDataService.SaveData();
            
            var scene = SceneManager.LoadSceneAsync(sceneBuildIndex);

            scene.allowSceneActivation = false;
            _contextService.UpdateGameContext(_loadingContext);

            yield return new WaitForSeconds(1.0f);                // ! FIXME - Delete this in an actual game

            while (scene.progress < 0.9f)
                yield return null;                              // Add delay to the scene load while is not ready
                
            scene.allowSceneActivation = true;

            Logs.SystemLog($"Level Loader : Scene \"{SceneManager.GetSceneByBuildIndex(sceneBuildIndex).name}\" loaded, make sure the scene has a SceneContextSetter");
        }
        
    }
}