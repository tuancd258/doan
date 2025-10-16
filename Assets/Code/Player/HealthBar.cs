using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform redBar;      // Khối đỏ
    public Transform target;      // Player / Enemy
    public Vector3 offset = new Vector3(0, 1f, 0); // đặt trên đầu

    private float originalWidth;  // chiều dài gốc khối đỏ
    private Vector3 originalPos;  // vị trí gốc

    void Awake()
    {
        if (redBar != null)
        {
            originalWidth = redBar.localScale.x;  // lưu scale gốc
            originalPos = redBar.localPosition;   // lưu vị trí gốc
        }
    }

    void Update()
    {
        if (target != null)
            transform.position = target.position + offset;
    }

    // percent: 0 → 1
    public void SetHealthPercent(float percent)
    {
        if (redBar == null) return;

        percent = Mathf.Clamp01(percent);

        // Thay đổi scale X khối đỏ theo phần trăm
        Vector3 scale = redBar.localScale;
        scale.x = originalWidth * percent;
        redBar.localScale = scale;

        // Đẩy khối đỏ về bên trái để giảm từ phải
        Vector3 pos = redBar.localPosition;
        pos.x = originalPos.x - (originalWidth - scale.x) / 2f;
        redBar.localPosition = pos;
    }
}
