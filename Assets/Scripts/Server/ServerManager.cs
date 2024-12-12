using System;
using System.Collections.Generic;
using System.Linq;
using Photon.Pun;
using Photon.Realtime;
using Teams;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Server {
    /**
     * expected to be in all scenes
     */
    public class ServerManager : MonoBehaviourPunCallbacks {
        private static List<RoomInfo> _roomInfosCache = new();

        public override void OnJoinedRoom() {
            PlayerTeamManager.SaveTeam(PhotonNetwork.CurrentRoom.PlayerCount == 2 ? ETeam.Attacker : ETeam.Defender);
            SceneManager.LoadScene("PreGameplay");
        }

        public static void CreateRoom(string n, int max = 2, Action s = null) {
            if (PhotonNetwork.CreateRoom(n, new RoomOptions {
                    MaxPlayers = max
                })) s?.Invoke();
            else Gui.Alert("Failed creating room");
        }

        /**
         * no PhotonNetwork.Get(string name) ??? stupid
         */
        public static bool RoomExistsCached(string n) {
            n = n.ToLower();
            return _roomInfosCache.Any(t => t.Name.ToLower().Equals(n));
        }

        public override void OnRoomListUpdate(List<RoomInfo> roomList) {
            _roomInfosCache = roomList;
        }

        public static void JoinRandom() {
            // maybe add params? idk
            PhotonNetwork.JoinRandomRoom();
        }

        /**
         * assuming the room was already validated
         */
        public static void Join(string n, Action s = null) {
            if (!PhotonNetwork.JoinRoom(n)) Gui.Alert("Failed to join, room might be full");
            else s?.Invoke();
        }

        public static void OnCurrentRoom(Action<Room> a) {
            if (PhotonNetwork.CurrentRoom == null) return;
            a.Invoke(PhotonNetwork.CurrentRoom);
        }

        public static GameObject Spawn(string n, Vector3? pos = null) {
            return PhotonNetwork.Instantiate(n, pos ?? Vector3.zero, Quaternion.identity);
        }
    }
}