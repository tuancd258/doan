using UnityEngine;

[CreateAssetMenu(menuName = "CREATER ITEM/ShopItem")]
public class ShopItemData : ScriptableObject
{
    public string itemName;
    public Sprite icon;
    public int price;

    [TextArea] public string description;

    // Stats (có thể mở rộng thêm)
    public int attack;
    public float attackSpeed;
    public int armor;
}
