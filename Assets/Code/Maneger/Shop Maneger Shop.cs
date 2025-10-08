using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance;

    [SerializeField] GameObject shopPanel;
    [SerializeField] Transform itemContainer;   
    [SerializeField] GameObject itemSlotPrefab;
    [SerializeField] TextMeshProUGUI goldText;
    [SerializeField] public Button NEXTWAVE;
    [SerializeField] public  Button ROLL;
    [SerializeField] int roll = 1;


    public int playerGold = 100;

    List<ShopItemData> allItems = new List<ShopItemData>();
    List<GameObject> currentSlots = new List<GameObject>();
    List<WeaponData> inventory = new List<WeaponData>();

    private void Awake()

    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // Load tất cả Item từ thư mục Resources/Items
        allItems.AddRange(Resources.LoadAll<ShopItemData>("Items"));
        inventory.AddRange(Resources.LoadAll<WeaponData>("Weapon/Meele"));
        inventory.AddRange(Resources.LoadAll<WeaponData>("Weapon/Ranger"));


        Debug.Log("Loaded items: " + allItems.Count);
        UpdateGoldUI();
        OpenShop();
        NEXTWAVE.onClick.AddListener(() => { Debug.Log("NextWave CLICKED"); });
        ROLL.onClick.AddListener(() => { Debug.Log("Roll CLICKED"); });
        NEXTWAVE.onClick.AddListener(nextWave);
        ROLL.onClick.AddListener(Roll);
    }

    public void OpenShop()

    {
        roll = 1;
        shopPanel.SetActive(true);
        GenerateShop();
    }

    public void CloseShop()
    {
        shopPanel.SetActive(false);
        ClearShop();
    }

     public void GenerateShop()
    {
        ClearShop();
            Debug.Log("Generating shop with " + allItems.Count + " items");

        for (int i = 0; i < 5; i++)
        {
            bool spawnWeapon = Random.value > 0.5f; // 50% là vũ khí, 50% là item

            if (spawnWeapon && inventory.Count > 0)
            {
                WeaponData weapon = inventory[Random.Range(0, inventory.Count)];
                GameObject slot = Instantiate(itemSlotPrefab, itemContainer);
                slot.GetComponent<ItemSlot>().SetupWeapon(weapon, this);
                currentSlots.Add(slot);
            }
            else if (allItems.Count > 0)
            {
                ShopItemData item = allItems[Random.Range(0, allItems.Count)];
                GameObject slot = Instantiate(itemSlotPrefab, itemContainer);
                slot.GetComponent<ItemSlot>().Setup(item, this);
                currentSlots.Add(slot);
            }
        }
    }
    public void nextWave()
    {
        CloseShop();
        Debug.Log("Next wave start!");

        if (SceneManager.GetSceneByName("ShopTest").isLoaded)
        {
            SceneManager.UnloadSceneAsync("ShopTest").completed += (op) =>
            {
                if (Manegerentity.instance != null)
                {
                    Manegerentity.instance.StartNextWave();   // chỉ gọi 1 lần
                }
            };
        }
    }

    void ClearShop()
    {
        foreach (GameObject slot in currentSlots)
            Destroy(slot);
        currentSlots.Clear();
    }

    public bool BuyItem(ShopItemData item)
    {
        if (playerGold >= item.price)
        {
            playerGold -= item.price;
            UpdateGoldUI();
          addToinventory(item);
            Debug.Log("Bought item: " + item.itemName);
            return true;

            // TODO: gán item cho PlayerWeapons
        }
        else
        {
            Debug.Log("Not enough gold!");
            return false;
        }
    }

    public void Roll()
    {
               if (playerGold >= roll  )
        {
            playerGold -= roll;
            roll++;
            UpdateGoldUI();
            GenerateShop();
            Debug.Log("Rolled shop items");
        }
        else
        {
            
            Debug.Log("Not enough gold to roll!");
            return;
        }
    }
    void UpdateGoldUI()
    {
        goldText.text = "Gold: " + playerGold;
    }
    void addToinventory(ShopItemData item)
    {
 
    }
    public bool BuyWeapon(WeaponData weapon)
    {
        if (playerGold >= weapon.price)
        {
            playerGold -= weapon.price;
            UpdateGoldUI();

            addWeaponToInventory(weapon);

            Debug.Log($"🪓 Bought weapon: {weapon.weaponName}");
            return true;
        }
        else
        {
            Debug.Log("💰 Not enough gold to buy weapon!");
            return false;
        }
    }

    void addWeaponToInventory(WeaponData weapon)
    {
       
        PlayerAttack player = FindObjectOfType<PlayerAttack>();

        if (player == null)
        {
            Debug.LogError("❌ Không tìm thấy PlayerAttack trong scene!");
            return;
        }

        if (weapon == null)
        {
            Debug.LogError("❌ Weapon bị null khi mua!");
            return;
        }

        // Thêm vũ khí vào người chơi
        player.AddWeapon(weapon);

        Debug.Log($"✅ Đã thêm {weapon.weaponName} vào người chơi.");
    }






}
