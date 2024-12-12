using Attributes;
using Teams;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scenes.Gameplay.Times {
    public class TimeManager : MonoBehaviour {
        [SerializeField] protected int timeSeconds;
        [SerializeField] protected TMP_Text timeText;

        [Ignore] public float _rTime; // public for testing
        [Ignore] private string _timeString;

        private void Awake() {
            Reflection.ValidateClassFields(this);
        }

        private void Start() {
            _rTime = timeSeconds;
        }

        private void Update() {
            if (_rTime > 0)
            {
                _rTime -= Time.deltaTime;
                UpdateTime();
            }
            else
            {
                PlayerTeamManager.SaveTeamWinner("Defender");
                SceneManager.LoadScene("PostGameplay");
            }
        }

        private void UpdateTime() {
            int minutes = Mathf.FloorToInt(_rTime / 60);
            int seconds = Mathf.FloorToInt(_rTime % 60);

            timeText.text = $"Time Remaining: {minutes:D2}:{seconds:D2}";
        }
    }
}