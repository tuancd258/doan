using UnityEngine;

public class test : MonoBehaviour
{
    public Transform player;
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float speed = 3f;
    public CircleCollider2D attackCollider;
    public float attackRange = 1f;   // phạm vi tấn công
    public int damage = 10;             // sát thương gây ra
    public float attackCooldown = 1f;   // 1 giây 1 lần tấn công
    private float lastAttackTime;

    void Start()
    {
        attackCollider= GetComponent<CircleCollider2D>();
        attackRange= attackCollider.radius;
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null) player = p.transform;
    }

    void Update()
    {
        if (player == null) return;

        // 1️⃣ Di chuyển về phía player
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

    // 3️⃣ Optional: hiển thị phạm vi tấn công
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
