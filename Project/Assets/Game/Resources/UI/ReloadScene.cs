using UnityEngine.SceneManagement;
using UnityEngine;

namespace Game {

    internal sealed class ReloadScene : MonoBehaviour {

        public void ReloadLevel() {
            SceneManager.LoadScene("Testing");
        }

    }
}
