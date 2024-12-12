using Scenes.Gameplay.Enemies;
using Tools;
using UnityEngine;

namespace Scenes.Gameplay.Towers {
    public class Bullet : MonoBehaviour {
        [SerializeField] private float speed = 0.2f;
        [SerializeField] private float maxAge = 3;
        [SerializeField] private int damage = 1;
        private float _age;

        private Vector3 _targetPos;

        private void Update() {
            _age += Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, speed * Time.deltaTime);

            if (_age >= maxAge || GeneralUtils.Distance(transform.position, _targetPos) <= 0.01f) Die();
        }

        private void OnTriggerEnter2D(Collider2D o) {
            Enemy e = o.gameObject.GetComponent<Enemy>();
            if (e == null) return;
            e.Hurt(damage);
            Die();
        }

        public void SetTargetPos(Vector3 t) {
            _targetPos = t;
        }

        private void Die() {
            Destroy(gameObject);
        }
    }
}