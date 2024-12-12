using Server;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.PreGameplay {
    public class PreGameplayUI : AbstractUI<PreGameplayUI> {
        private void Update() {
            ServerManager.OnCurrentRoom(r => {
                if (r.PlayerCount >= 2) SceneManager.LoadScene("Gameplay");
            });
        }

        protected override PreGameplayUI GetObj() {
            return this;
        }
    }
}