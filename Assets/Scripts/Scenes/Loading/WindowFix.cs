using UnityEngine;

namespace Scenes.Loading {
    public class WindowFix : MonoBehaviour {
        private void Awake() {
            Screen.fullScreen = false;
            Screen.SetResolution(1280, 720, false);
        }
    }
}