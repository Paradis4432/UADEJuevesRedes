using System;
using Teams;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Gameplay {
    public class GameManager : MonoBehaviour {
        // TODO chat manager

        private static int _coins = 100;
        private static int _lives = 3;

        [SerializeField] private Canvas defenderCanvas;
        [SerializeField] private Canvas attackerCanvas;

        private void Update() {
            if (_lives > 0) return;
            PlayerTeamManager.SaveTeamWinner("Attacker");
            SceneManager.LoadScene("PostGameplay");
        }

        private void Start() {
            Reflection.ValidateClassFields(this);

            if (PlayerTeamManager.GetPlayerTeam() == ETeam.Defender) _coins *= 3; // more coins for defender

            Canvas c = null;
            switch (PlayerTeamManager.GetPlayerTeam())
            {
                case ETeam.Defender:
                    c = defenderCanvas;
                    break;
                case ETeam.Attacker:
                    c = attackerCanvas;
                    break;
                case ETeam.Spectator:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (c != null) c.gameObject.SetActive(true);

            InvokeRepeating(nameof(GivePassiveCoins), 0, 5.0f);
        }

        private void GivePassiveCoins() {
            UpdateCoins(10);
        }

        // breaks when user is both defender and attacker, which should never happen
        public static bool CanBuy(int req) {
            return _coins >= req;
        }

        public static void UpdateCoins(int a) {
            _coins += a; // works with negative values 
        }

        public static int GetCoins() {
            return _coins;
        }

        public static int GetLives() {
            return _lives;
        }

        public static void HurtDefender() {
            _lives--;
        }

        public static void Clear() {
            _lives = 3;
            _coins = 100;
        }
    }
}