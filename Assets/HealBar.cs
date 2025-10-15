using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class HealBar : MonoBehaviour
{
    public Image fillImage;      // Image Fill (m�u ??)
    public Vector3 offset = new Vector3(0, 1f, 0);

    void Update()
    {
        transform.position = transform.position + offset;
    }

    // Ch? nh?n t? l? m�u c�n l?i 0?1
    public void SetHealthPercent(float percent)
    {
        percent = Mathf.Clamp01(percent); // ??m b?o 0?1
        if (fillImage != null)
            fillImage.fillAmount = percent;
    }
}
