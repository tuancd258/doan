using System;
using UnityEngine;

public class Player : Character
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    public float speed = 5f;
    private void Start()
    {
           
    }
    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;

        // Di chuyển
        transform.position += (Vector3)(dir*speed*Time.deltaTime);
        // Lật nhân vật
        if (dir.x != 0)
        {
            spriteRenderer.flipX = dir.x < 0;
        }
    }
}
