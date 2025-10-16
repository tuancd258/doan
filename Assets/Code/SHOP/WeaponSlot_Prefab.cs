// File: WeaponSlotButton.cs
using UnityEngine;

public class WeaponSlotButton : MonoBehaviour
{
    [Tooltip("Kéo GameObject DeleteButton con vào đây")]
    public GameObject deleteButtonObject; 

    private int weaponIndex;

    public void SetIndex(int index)
    {
        this.weaponIndex = index;
    }

  
    public void OnPrimarySlotClicked()
    {
        
        if (SlotItem.Instance != null)
        {
            SlotItem.Instance.ShowDeleteConfirmation(this);
        }
    }

   
    public void OnConfirmDeleteClicked()
    {
        if (PlayerAttack.Instance != null)
        {
            PlayerAttack.Instance.RemoveWeapon(weaponIndex);
        }
      
        HideDeleteButton();
    }


    public void ShowDeleteButton()
    {
        if (deleteButtonObject != null)
        {
            deleteButtonObject.SetActive(true);
        }
    }


    public void HideDeleteButton()
    {
        if (deleteButtonObject != null)
        {
            deleteButtonObject.SetActive(false);
        }
    }
}