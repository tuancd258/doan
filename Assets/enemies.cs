using UnityEngine;

public class enemies : MonoBehaviour
{
    private Transform player; // tốt hơn dùng Transform
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float speed = 3f;
    public void Start()
    {
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        player = p.transform;
    }
    void Update()
    {
        if (player != null)
        {
            // Tính hướng từ enemy đến player
            Vector2 direction = (player.position - transform.position).normalized;

            // Di chuyển enemy về hướng player
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            if (direction.x != 0) { 
                spriteRenderer.flipX = direction.x<0;
            }

            // Optional: xoay sprite về hướng di chuyển
            //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //transform.rotation = Quaternion.Euler(0f, 0f, angle);
        }
    }
}
