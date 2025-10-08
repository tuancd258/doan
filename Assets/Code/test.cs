using UnityEngine;

public class test : MonoBehaviour
{
    private Transform player;
  
    public float speed = 3f;
    [SerializeField] private bool isFacingRight = true;
    void Start()
    {
        // Tìm Player theo tag
        GameObject p = GameObject.FindGameObjectWithTag("Player");
        if (p != null)
        {
            player = p.transform;
        }
    }

    void Update()
    {
        if (player != null)
        {
            Vector2 direction = (player.position - transform.position).normalized;

            transform.position += (Vector3)(direction * speed * Time.deltaTime);
            if (direction.x > 0 && !isFacingRight) Flip();
            else if (direction.x < 0 && isFacingRight) Flip();

        }
    }
    void Flip()
    {
        isFacingRight = !isFacingRight; // đảo hướng
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
        
}
