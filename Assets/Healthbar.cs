using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public Transform redBar;      // khối đỏ
    public float fullWidth = 2f;
    public Vector3 offset = new Vector3(0, 1f, 0); // trên đầu target
    private Transform target;     // parent Heal

    void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }

    public void SetTarget(Transform t)
    {
        target = t;
    }

    // Chỉ nhận tỉ lệ máu còn lại 0→1
    public void SetHealthPercent(float percent)
    {
        percent = Mathf.Clamp01(percent);
        Vector3 scale = redBar.localScale;
        scale.x = fullWidth * percent;
        redBar.localScale = scale;

        // kéo khối đỏ từ bên phải
        Vector3 pos = redBar.localPosition;
        pos.x = -(fullWidth - scale.x) / 2f;
        redBar.localPosition = pos;
    }
}
