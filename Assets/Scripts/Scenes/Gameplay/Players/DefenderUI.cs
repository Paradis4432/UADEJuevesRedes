using System.Collections.Generic;
using System.Linq;
using Attributes;
using Photon.Pun;
using Scenes.Gameplay.Repo;
using Scenes.Gameplay.Towers;
using Server;
using TMPro;
using Tools;
using Tools.Serdes.Impls;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Scenes.Gameplay.Players {
    public class DefenderUI : AbstractPlayerUI<DefenderUI> {
        [SerializeField] private TMP_Text livesText;

        [SerializeField] private Button t;

        // can be removed if extracted the source image from Button -> Image since they are the same
        //[SerializeField] private TileBase t0Tile;
        [SerializeField] private List<TileBase> uTiles = new();
        [SerializeField] private List<TileBase> tTiers = new();

        [SerializeField] private Tower tS;
        [SerializeField] private int tCs;

        [Ignore] private readonly Dictionary<Vector3Int, Tower> _towers = new();

        private bool _c;

        private void Update() {
            // TODO replace with event queue
            livesText.text = $"Lives left: {GameManager.GetLives()}";

            if (!Input.GetMouseButtonDown(0) || !_c) return;
            _c = false;

            if (!GameManager.CanBuy(tCs))
            {
                Gui.Alert("Not enough coins!");
                return;
            }

            Vector3 wp = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3Int wtc = tilemap.WorldToCell(wp);
            int[] wtcs = Vector3IntSerdes.GetImpl().Serialize(wtc);


            TileBase cTile = tilemap.GetTile(wtc);

            // cTile is the current tile, check name, find next, upgrade if found, alert if no upgrade left

            if (!uTiles.Contains(cTile) && !tTiers.Any(tileBase => tileBase.name.Equals(cTile.name)))
            {
                Gui.Alert("Cant place tower here!");
                return;
            }

            TileBase nTile;
            int idx = tTiers.FindIndex(tile => tile.name.Equals(cTile.name));

            if (idx == -1)
            {
                // spawn tier 0 tower
                nTile = tTiers[0];
                // TODO replace with TowerFactory
                Tower tc = ServerManager.Spawn(tS.name, wp).GetComponent<Tower>();

                _towers.Add(wtc, tc);
            }
            else if (idx == tTiers.Count - 1)
            {
                Gui.Alert("Cant upgrade this tower more!");
                return;
            }
            else
            {
                nTile = tTiers[idx + 1];
                // get tower script, upgrade, maybe use event queue?
                Gui.Alert("Upgraded tower!");
                // em ambos lados find script -> mejorar
                // spawnear tower con id y rpc -> mejorar id


                PhotonView.Get(this).RPC("UpgradeTower", RpcTarget.AllBuffered,
                    _towers[wtc].GetID()); 
            }

            GameManager.UpdateCoins(-tCs);
            PhotonView.Get(this).RPC("SetTileRPC", RpcTarget.AllBuffered,
                wtcs, nTile.name);
        }

        protected override void Awake() {
            base.Awake();

            t.onClick.AddListener(() => {
                Gui.Alert("click where to place a tower");
                _c = true;
            });
        }

        [PunRPC]
        public void SetTileRPC(int[] poss, string n) {
            Vector3Int wtp = Vector3IntSerdes.GetImpl().Deserialize(poss);

            tilemap.SetTile(wtp, Resources.Load<TileBase>(n));
        }

        [PunRPC]
        public void UpgradeTower(int id) {
            TowerRepository.Towers[id].Upgrade();
        }

        protected override DefenderUI GetObj() {
            return this;
        }
    }
}