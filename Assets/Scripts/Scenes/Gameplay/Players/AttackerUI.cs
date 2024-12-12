using Server;
using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Scenes.Gameplay.Players {
    public class AttackerUI : AbstractPlayerUI<AttackerUI> {
        [SerializeField] private Button a0;
        [SerializeField] private GameObject a0S;
        [SerializeField] private int a0Cs;


        protected override void Awake() {
            base.Awake();

            a0.onClick.AddListener(() => {
                // spawn new enemy
                // TODO replace with EnemyFactory, use pool
                if (!GameManager.CanBuy(a0Cs))
                {
                    Gui.Alert("Not enough coins!");
                    return;
                }

                //Instantiate(a0S, new Vector3(1000, 1000, -1), Quaternion.identity).SetActive(true);
                ServerManager.Spawn(a0S.name).SetActive(true);
                GameManager.UpdateCoins(-a0Cs);
            });
        }

        protected override AttackerUI GetObj() {
            return this;
        }
    }
}