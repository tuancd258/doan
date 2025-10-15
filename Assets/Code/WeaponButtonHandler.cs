using UnityEngine;
using UnityEngine.EventSystems; // Thư viện cho sự kiện chuột

public class WeaponButtonHandler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Kéo file ScriptableObject WeaponData tương ứng vào đây
    public WeaponData weaponData;

    // Hàm này tự động được gọi khi chuột di vào đối tượng này
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (weaponData != null)
        {
            // Gọi hàm ShowTooltip từ Singleton và truyền data của vũ khí này vào
            WeaponTooltip.Instance.ShowTooltip(weaponData);
        }
    }

    // Hàm này tự động được gọi khi chuột di ra khỏi đối tượng này
    public void OnPointerExit(PointerEventData eventData)
    {
        // Gọi hàm HideTooltip từ Singleton
        WeaponTooltip.Instance.HideTooltip();
    }
}