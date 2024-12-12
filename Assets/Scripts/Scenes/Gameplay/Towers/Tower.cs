using Attributes;
using Scenes.Gameplay.Enemies;
using Scenes.Gameplay.Repo;
using Tools;
using UnityEngine;

namespace Scenes.Gameplay.Towers {
    public class Tower : MonoBehaviour {
        [SerializeField] private Bullet bullet;

        [Ignore] private int _id;
        [Ignore] public float _range = 0.6f;
        [Ignore] public float _shootInterval = 1;
        [Ignore] private int _level;
        [Ignore] private float _shootDelay;
        [Ignore] private Enemy _target;

        private void Awake() {
            Reflection.ValidateClassFields(this);

            TowerRepository.Towers[_id] = this;
        }


        private void Update() {
            _shootDelay += Time.deltaTime;
            if (_shootDelay > _shootInterval) _shootDelay = 0f;
            else return;
            // search for targets nearby and attack
            // if close spawn bullet
            _target = EnemyRepository.FindNearestOf(gameObject, _range);

            if (!_target) return;

            Instantiate(bullet, transform.position, Quaternion.identity).SetTargetPos(_target.transform.position);
        }

        public void Upgrade() {
            switch (_level)
            {
                case 0: // when first placed
                    _range = 0.6f;
                    _shootInterval = 1;
                    break;
                case 1:
                    _shootInterval = 0.8f;
                    break;
                case 2:
                    _shootInterval = 0.5f;
                    break;
                case 3:
                    _range = 1.2f;
                    break;
                case 4:
                    _range = 1.5f;
                    _shootInterval = 0.4f;
                    break;
                case 5:
                    _shootInterval = 0.2f;
                    break;
            }

            _level++;
        }

        public int GetID() {
            return _id;
        }
    }
}