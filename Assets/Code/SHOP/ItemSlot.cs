using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI description;
    [SerializeField] Button buyButton;

    ShopItemData currentItem;
    ShopManager shopManager;

    public void Setup(ShopItemData item, ShopManager manager)
    {
        currentItem = item;
        shopManager = manager;

        if (icon) icon.sprite = item.icon;
        if (nameText) nameText.text = item.itemName;
        if (priceText) priceText.text = item.price.ToString();
        if (description) description.text = item.description;


        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => shopManager.BuyItem(item));





    }
    public void SetupWeapon(WeaponData weapon, ShopManager manager)
    {
        shopManager = manager;

        // ✅ Lấy icon từ WeaponData
        icon.sprite = weapon.icon;

        // Nếu vẫn null, kiểm tra spriteRenderer
        if (icon.sprite == null)
            Debug.LogWarning($"⚠ Weapon {weapon.weaponName} chưa có icon hoặc sprite!");

        nameText.text = weapon.weaponName;
        priceText.text = weapon.price.ToString();
        description.text = $"Damage: {weapon.damage}\nCooldown: {weapon.cooldown}s";

        buyButton.onClick.RemoveAllListeners();
        buyButton.onClick.AddListener(() => shopManager.BuyWeapon(weapon));
    }



}
