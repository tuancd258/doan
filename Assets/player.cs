using UnityEngine;

public class Player: MonoBehaviour 
{
    private SpriteRenderer spriteRenderer;
    public SpriteRenderer spriteMap;
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

        float xplayer = spriteRenderer.bounds.size.x / 2f;
        float yplayer = spriteRenderer.bounds.size.y / 2f;

        float minX = spriteMap.bounds.min.x + xplayer;
        float maxX = spriteMap.bounds.max.x - xplayer;
        float minY = spriteMap.bounds.min.y + yplayer;
        float maxY = spriteMap.bounds.max.y - yplayer;
        Vector3 newpos = transform.position + (Vector3)(dir * speed * Time.deltaTime);
        newpos.x = Mathf.Clamp(newpos.x, minX, maxX);
        newpos.y = Mathf.Clamp(newpos.y, minY, maxY);
        transform.position = newpos;

        //transform.position += (Vector3)(dir * speed * Time.deltaTime);

        if (dir.x != 0)
        {
            spriteRenderer.flipX = dir.x < 0;
        }
    }
}
