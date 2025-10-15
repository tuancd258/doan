using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform redBar;  // Red rectangle
    public float fullWidth = 2f; // chiều dài tối đa
    public Vector3 offset = new Vector3(0, 1f, 0); // đặt trên đầu

    public Transform target;  // player hoặc enemy

    void Update()
    {
        if (target != null)
        {
            // di chuyển thanh máu theo target
            transform.position = target.position + offset;
        }
    }

    // percent: 0 → 1
    public void SetHealthPercent(float percent)
    {
        percent = Mathf.Clamp01(percent);

        // Thay đổi scale X khối đỏ
        Vector3 scale = redBar.localScale;
        scale.x = fullWidth * percent;
        redBar.localScale = scale;

        // Đẩy khối đỏ về bên trái để giảm từ phải
        Vector3 pos = redBar.localPosition;
        pos.x = -(fullWidth - scale.x) / 2f;
        redBar.localPosition = pos;
    }
}
