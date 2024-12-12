using Photon.Pun;
using Scenes.Gameplay;
using Scenes.Gameplay.Repo;
using Teams;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Scenes.PostGameplay {
    public class PostGameplayManager : MonoBehaviour {
        [SerializeField] private TMP_Text t;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button quitButton;

        private void Start() {
            Reflection.ValidateClassFields(this);

            EnemyRepository.Clear();
            GameManager.Clear();

            PhotonNetwork.LeaveRoom();

            menuButton.onClick.AddListener(() => {
                PhotonNetwork.JoinLobby();
                SceneManager.LoadScene("MainMenu");
            });
            
            

            quitButton.onClick.AddListener(Application.Quit);

            t.SetText("Winner: " + PlayerTeamManager.GetWinnerTeam());
        }
    }
}