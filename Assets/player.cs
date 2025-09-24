using UnityEngine;

public class Player : Character
{
    private SpriteRenderer spriteRenderer;
    public float speed = 5f;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (GameManager.Instance != null && GameManager.Instance.SelectedCharacterTexture != null)
        {
            Texture2D tex = GameManager.Instance.SelectedCharacterTexture;

            // Tạo sprite từ Texture2D
            Sprite newSprite = Sprite.Create(
                tex,
                new Rect(0, 0, tex.width, tex.height),
                new Vector2(0.5f, 0.5f)
            );

            spriteRenderer.sprite = newSprite;
            Debug.Log("Player load sprite từ Texture2D: " + GameManager.Instance.SelectedCharacter);
        }
        else
        {
            Debug.LogWarning("Không tìm thấy Texture2D trong GameManager!");
        }
    }

    private void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        Vector2 dir = new Vector2(x, y).normalized;

        transform.position += (Vector3)(dir * speed * Time.deltaTime);

        if (dir.x != 0)
        {
            spriteRenderer.flipX = dir.x < 0;
        }
    }
}
