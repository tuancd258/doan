// File: WeaponSlotButton.cs
using UnityEngine;

public class WeaponSlotButton : MonoBehaviour
{
    [Tooltip("Kéo GameObject DeleteButton con vào đây")]
    public GameObject deleteButtonObject; // Ô để kéo nút Delete vào

    private int weaponIndex;

    public void SetIndex(int index)
    {
        this.weaponIndex = index;
    }

    // Được gọi khi click vào ô slot (click lần 1)
    public void OnPrimarySlotClicked()
    {
        // Báo cho SlotItem Manager biết để hiện nút xác nhận
        if (SlotItem.Instance != null)
        {
            SlotItem.Instance.ShowDeleteConfirmation(this);
        }
    }

    // Được gọi khi click vào nút "Delete" (click lần 2)
    public void OnConfirmDeleteClicked()
    {
        if (PlayerAttack.Instance != null)
        {
            PlayerAttack.Instance.RemoveWeapon(weaponIndex);
        }
        // Sau khi xóa, ẩn nút delete đi
        HideDeleteButton();
    }

    // Hàm để hiện nút delete
    public void ShowDeleteButton()
    {
        if (deleteButtonObject != null)
        {
            deleteButtonObject.SetActive(true);
        }
    }

    // Hàm để ẩn nút delete
    public void HideDeleteButton()
    {
        if (deleteButtonObject != null)
        {
            deleteButtonObject.SetActive(false);
        }
    }
}