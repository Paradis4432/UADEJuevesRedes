using System;
using Server;
using Teams;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.MainMenu {
    /*
     * create room <name
     * join room <name
     * join random room
     *
     */
    public class MenuUI : AbstractUI<MenuUI> {
        [SerializeField] private TMP_InputField input;
        [SerializeField] private Button createInput;
        [SerializeField] private Button joinInput;

        protected override void Awake() {
            base.Awake();

            createInput.onClick.AddListener(() => ForValidInput(i => {
                if (ServerManager.RoomExistsCached(i)) Gui.Alert("Room is already created");
                else
                    ServerManager.CreateRoom(i, 2, () => {
                        Gui.Alert(() => {
                            PlayerTeamManager.SaveTeam(ETeam.Defender);
                            SceneManager.LoadScene("PreGameplay");
                        }, "Created room: " + i + " as defender");
                    });
            }));

            joinInput.onClick.AddListener(() => ForValidInput(i => {
                if (ServerManager.RoomExistsCached(i))
                    ServerManager.Join(i, () => {
                        Gui.Alert(() => {
                            SceneManager.LoadScene("PreGameplay");
                            PlayerTeamManager.SaveTeam(ETeam.Attacker);
                        }, "Joined: " + i + " as attacker");
                    });
                else Gui.Alert("Room doesnt exist");
            }));
        }

        protected override MenuUI GetObj() {
            return this;
        }

        private void ForValidInput(Action<string> a) {
            if (input.text.Length > 0) a?.Invoke(input.text);
            else Gui.Alert("invalid input");
        }
    }
}