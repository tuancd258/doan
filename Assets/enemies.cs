using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Transform player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float speed = 3f;
    public CircleCollider2D Collider2D;
    private float attackRange = 1f;    
    public int damage = 10;             // sát thương gây ra
    public float attackCooldown = 1f;   // 1 giây 1 lần tấn công
    private float lastAttackTime;

    void Start()
    {
        Collider2D = GetComponent<CircleCollider2D>();
        attackRange=Collider2D.radius;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;

    }

    void Update()
    {
        if (player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;
        transform.position += (Vector3)(direction * speed * Time.deltaTime);

        if (direction.x != 0)
        {
            spriteRenderer.flipX = direction.x < 0;
        }

        // 2️⃣ Kiểm tra khoảng cách → gây damage
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= attackRange && Time.time - lastAttackTime >= attackCooldown)
        {
            PlayerStats ps = player.GetComponent<PlayerStats>();
            if (ps != null)
            {
                ps.TakeDamage(damage);
                lastAttackTime = Time.time;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
