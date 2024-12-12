using System.Diagnostics;
using System.IO;
using Teams;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Editor {
    public abstract class Builder {
        [MenuItem("Tools/Build")]
        public static void BuildGame() {
            const string b = "Build/UADEJuevesRedes.exe";

            BuildPlayerOptions bOpts = new() {
                scenes = new[] {
                    "Assets/Scenes/Loading.unity",
                    "Assets/Scenes/PreGameplay.unity",
                    "Assets/Scenes/MainMenu.unity",
                    "Assets/Scenes/Gameplay.unity",
                    "Assets/Scenes/PostGameplay.unity",
                },
                locationPathName = b,
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.Development | BuildOptions.AllowDebugging
            };

            BuildPipeline.BuildPlayer(bOpts);

            string absoluteBuildPath = Path.GetFullPath(b);

            for (int i = 0; i < 2; i++)
            {
                Process proc = new();
                proc.StartInfo.FileName = absoluteBuildPath;
                proc.Start();
            }
        }

        [MenuItem("Tools/WinDefender")]
        public static void WinDefender() {
            PlayerTeamManager.SaveTeamWinner("Defender");
            SceneManager.LoadScene("PostGameplay");
        }

        [MenuItem("Tools/WinAttacker")]
        public static void WinAttacker() {
            PlayerTeamManager.SaveTeamWinner("Attacker");
            SceneManager.LoadScene("PostGameplay");
        }
    }


    [InitializeOnLoad]
    public class BackgroundBuildListener {
        private static readonly string P = Path.Combine(Application.dataPath, "build.signal");

        static BackgroundBuildListener() {
            EditorApplication.update += CheckForBuildSignal;
        }

        private static void CheckForBuildSignal() {
            if (!File.Exists(P)) return;
            File.Delete(P);
            Builder.BuildGame();
        }
    }
}