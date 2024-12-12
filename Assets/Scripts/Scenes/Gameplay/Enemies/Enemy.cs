using Attributes;
using Photon.Pun;
using Scenes.Gameplay.Path;
using Scenes.Gameplay.Repo;
using Tools;
using UnityEngine;

namespace Scenes.Gameplay.Enemies {
    public class Enemy : MonoBehaviour {
        [SerializeField] private int hp = 10;
        [SerializeField] private float speed = 0.1f;
        [SerializeField] private int coinsForReachingEnd = 10;
        [SerializeField] private int coinsForDie = 1;
        [SerializeField] private int coinsReward = 10;

        [Ignore] private Transform _currentTarget;

        private PhotonView _pv;

        private void Awake() {
            _pv = gameObject.GetComponent<PhotonView>();

            Reflection.ValidateClassFields(this);

            EnemyRepository.Register(this);

            // not the best, works for now
            transform.position = PathManager.GetFirst().position;
            _currentTarget = PathManager.GetNext(PathManager.GetFirst());
        }

        private void Update() {
            if (hp <= 0)
            {
                Die();
                return;
            }

            // technically theres no need to fetch the next if done the math on how long it takes from point A to point B
            // to instead yield that time and then fetch, technically would work, TBD
            Vector3 position = transform.position;
            _currentTarget = PathManager.GetNext(_currentTarget, position);

            if (PathManager.IsLast(_currentTarget, transform.position))
            {
                PhotonView.Get(this).RPC("HurtDefender", RpcTarget.All);

                if (_pv.IsMine) GameManager.UpdateCoins(coinsForReachingEnd);

                Die();
                return;
            }

            position = Vector3.MoveTowards(position, _currentTarget.position, speed * Time.deltaTime);
            transform.position = position;
        }

        [PunRPC]
        public void HurtDefender() {
            GameManager.HurtDefender();
        }

        public void Hurt(int damage) {
            hp -= damage;
        }

        private void Die() {
            GameManager.UpdateCoins(coinsForDie);

            _pv.RPC("DestroyEnemy", RpcTarget.AllBuffered, gameObject.GetComponent<PhotonView>().ViewID);
        }

        [PunRPC]
        private void DestroyEnemy(int id) {
            PhotonView v = PhotonView.Find(id);
            if (v == null) return;

            if (v.IsMine) PhotonNetwork.Destroy(v.gameObject);
            EnemyRepository.Unregister(this);
        }

        public int GetCoinsReward() {
            return coinsReward;
        }
    }
}