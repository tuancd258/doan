using UnityEngine;

namespace Goldmetal.UndeadSurvivor
{
    public class Bullet : MonoBehaviour
    {
        public float speed = 10f;
        public float lifeTime = 2f;
        public float damage = 10f;

        private Vector2 direction;
        private float lifeTimer;

        void OnEnable()
        {
            lifeTimer = lifeTime; // reset timer khi lấy ra từ pool
        }

        public void SetDirection(Vector2 dir)
        {
            direction = dir.normalized;
        }

        void Update()
        {
            // di chuyển
            transform.Translate(direction * speed * Time.deltaTime);

            // countdown
            lifeTimer -= Time.deltaTime;
            if (lifeTimer <= 0f)
            {
                PoolManager.Instance.Despawn(gameObject); // Trả về pool
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Enemy"))
            {
                Heal enemyHealth = other.GetComponent<Heal>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(damage);
                }

                PoolManager.Instance.Despawn(gameObject); // Trả về pool
            }

            if (other.CompareTag("Wall"))
            {
                PoolManager.Instance.Despawn(gameObject); // Trả về pool
            }
        }
    }
}
