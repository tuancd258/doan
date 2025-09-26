using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private SpriteRenderer _SpriteRenderer;
    void Start()
    {
        _SpriteRenderer = GetComponent<SpriteRenderer>();
        Texture2D tex= GameManager.Instance.SelectedWeaponTexture;
        Sprite newSpite = Sprite.Create(
            tex,
            new Rect(0, 0, tex.width, tex.height),
            new Vector2(0.5f, 0.5f)
            );
        _SpriteRenderer.sprite = newSpite;  
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
