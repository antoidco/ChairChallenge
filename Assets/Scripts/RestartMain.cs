using UnityEngine;
using UnityEngine.SceneManagement;

namespace Com.Antoid.ChairChallenge {
    public class RestartMain : MonoBehaviour {
        public void OnRestart() {
            SceneManager.LoadScene(1);
        }
    }
}
