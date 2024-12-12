using System;
using System.Diagnostics;
using UnityEngine;

namespace Teams {
    public abstract class PlayerTeamManager {
        private static string KeyName => $"team_{InstanceID}";

        private static int InstanceID => Process.GetCurrentProcess().Id;

        public static ETeam GetPlayerTeam() {
            if (PlayerPrefs.HasKey(KeyName))
                return FromID(PlayerPrefs.GetInt(KeyName));

            throw new ArgumentNullException("team not found");
        }

        public static void SaveTeam(ETeam t) {
            SaveTeam(ToID(t));
        }

        public static bool HasTeam() {
            return PlayerPrefs.HasKey(KeyName);
        }

        public static void SaveTeamWinner(string w) {
            PlayerPrefs.SetString("winner", w);
        }

        public static string GetWinnerTeam() {
            return PlayerPrefs.GetString("winner");
        }

        private static void SaveTeam(int id) {
            PlayerPrefs.SetInt(KeyName, id);
            PlayerPrefs.Save();
        }

        private static int ToID(ETeam t) {
            return t switch {
                ETeam.Defender => 0,
                ETeam.Attacker => 1,
                ETeam.Spectator => 2,
                _ => throw new ArgumentOutOfRangeException(nameof(t), t, null)
            };
        }

        private static ETeam FromID(int id) {
            return id switch {
                0 => ETeam.Defender,
                1 => ETeam.Attacker,
                2 => ETeam.Spectator,
                _ => throw new ArgumentOutOfRangeException(id.ToString(), id, null)
            };
        }
    }


    public enum ETeam {
        Defender,
        Attacker,
        Spectator
    }
}