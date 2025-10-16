using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SlotItem : MonoBehaviour
{
    public static SlotItem Instance;

    [Header("UI Elements")]
    [SerializeField] private Transform slotContainer;
    [SerializeField] private GameObject slotPrefab;
    [SerializeField] private TextMeshProUGUI weaponCountText;

    private List<Image> slotIcons = new List<Image>();
    private List<GameObject> slotLocks = new List<GameObject>();
    private List<WeaponSlotButton> slotButtons = new List<WeaponSlotButton>();
    private const int maxSlots = 6;

    private WeaponSlotButton activeConfirmationSlot = null; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeSlots();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSlots()
    {
        for (int i = 0; i < maxSlots; i++)
        {
            GameObject slot = Instantiate(slotPrefab, slotContainer);

            Transform iconTransform = slot.transform.Find("Icon");
            if (iconTransform != null)
            {
                slotIcons.Add(iconTransform.GetComponent<Image>());
            }
            else
            {
                Debug.LogError($"Prefab của slot không tìm thấy đối tượng con tên là 'Icon'!");
            }

            Transform lockTransform = slot.transform.Find("Lock");
            if (lockTransform != null)
            {
                slotLocks.Add(lockTransform.gameObject);
            }
            else
            {
                Debug.LogError($"Prefab của slot không tìm thấy đối tượng con tên là 'Lock'!");
            }

            WeaponSlotButton buttonScript = slot.GetComponent<WeaponSlotButton>();
            if (buttonScript != null)
            {
                slotButtons.Add(buttonScript);
            }
            else
            {
                Debug.LogError($"Prefab của slot thiếu script 'WeaponSlotButton'!");
            }
        }
    }

    public void ShowDeleteConfirmation(WeaponSlotButton clickedSlot)
    {
        if (activeConfirmationSlot != null && activeConfirmationSlot != clickedSlot)
        {
            activeConfirmationSlot.HideDeleteButton();
        }
        clickedSlot.ShowDeleteButton();
        activeConfirmationSlot = clickedSlot;
    }

  
    public void HideActiveConfirmation()
    {
        if (activeConfirmationSlot != null)
        {
            activeConfirmationSlot.HideDeleteButton();
            activeConfirmationSlot = null;
        }
    }

    public void UpdateWeaponSlots(List<WeaponController> equippedWeapons)
    {
        HideActiveConfirmation(); // Luôn ẩn nút delete cũ khi cập nhật UI

        if (weaponCountText != null)
        {
            weaponCountText.text = $"Weapons ({equippedWeapons.Count}/{maxSlots})";
        }

        for (int i = 0; i < maxSlots; i++)
        {
            Button button = slotButtons[i].GetComponent<Button>();

            if (i < equippedWeapons.Count && equippedWeapons[i] != null && equippedWeapons[i].weaponData != null)
            {
                // ---- NẾU CÓ VŨ KHÍ ----
                WeaponData data = equippedWeapons[i].weaponData;

                slotIcons[i].sprite = data.GetIcon();
                slotIcons[i].color = Color.white;
                slotLocks[i].SetActive(false);

                button.interactable = true;
                slotButtons[i].SetIndex(i);

                button.onClick.RemoveAllListeners();
                
                button.onClick.AddListener(slotButtons[i].OnPrimarySlotClicked);
            }
            else
            {
         
                slotIcons[i].sprite = null;
                slotIcons[i].color = new Color(1, 1, 1, 0.3f);
                slotLocks[i].SetActive(true);
                button.interactable = false;

           
                slotButtons[i].HideDeleteButton();
            }
        }
    }
}