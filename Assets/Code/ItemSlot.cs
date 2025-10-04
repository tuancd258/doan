using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
}
