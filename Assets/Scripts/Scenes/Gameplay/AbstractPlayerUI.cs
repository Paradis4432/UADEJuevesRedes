using Teams;
using TMPro;
using Tools;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Scenes.Gameplay {
    public abstract class AbstractPlayerUI<TUI> : AbstractUI<TUI> where TUI : AbstractPlayerUI<TUI> {
        private const float UInt = 0.25f;

        // power ups
        // upgrades
        // coins
        [SerializeField] protected Tilemap tilemap;
        [SerializeField] protected Camera mainCamera;
        [SerializeField] protected TMP_Text coinsText;
        [SerializeField] protected string coinsTextPattern;
        private float _timer;

        protected override void Awake() {
            Reflection.ValidateClassFields(this);
            base.Awake();
        }

        private void Start() {
            Gui.Alert("You are on team: " + PlayerTeamManager.GetPlayerTeam());
        }

        private void Update() {
            _timer += Time.deltaTime;
            if (!(_timer >= UInt)) return;
            _timer = 0f;
            coinsText.text = coinsTextPattern.Replace("%coins%", GameManager.GetCoins().ToString());
        }
    }
}