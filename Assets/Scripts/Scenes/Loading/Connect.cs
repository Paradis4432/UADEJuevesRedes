using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Loading {
    public class Connect : MonoBehaviourPunCallbacks {
        [SerializeField] private bool testing;

        private void Start() {
            PhotonNetwork.ConnectUsingSettings();
        }

        public override void OnConnectedToMaster() {
            Debug.Log("Connected to Photon Master Server");

            if (testing)
            {
                Debug.Log("test mode enabled, joining room");
                PhotonNetwork.JoinOrCreateRoom("test", new RoomOptions {
                    MaxPlayers = 2
                }, TypedLobby.Default);
            }
            else
            {
                PhotonNetwork.JoinLobby();
            }
        }

        public override void OnJoinedLobby() {
            if (!testing) SceneManager.LoadScene("MainMenu");
        }
    }
}