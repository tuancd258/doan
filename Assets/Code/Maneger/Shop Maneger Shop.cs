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

    private void Awake()

    {
        if (Instance == null) Instance = this;
    }

    private void Start()
    {
        // Load tất cả Item từ thư mục Resources/Items
        allItems.AddRange(Resources.LoadAll<ShopItemData>("Items"));
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
            ShopItemData randomItem = allItems[Random.Range(0, allItems.Count)];
            Debug.Log("Creating slot for: " + randomItem.itemName);
            GameObject slot = Instantiate(itemSlotPrefab, itemContainer);
            slot.GetComponent<ItemSlot>().Setup(randomItem, this);
            currentSlots.Add(slot);
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

    public void BuyItem(ShopItemData item)
    {
        if (playerGold >= item.price)
        {
            playerGold -= item.price;
            UpdateGoldUI();
            Debug.Log("Bought: " + item.itemName);

            // TODO: gán item cho PlayerWeapons
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    public void Roll()
    {
               if (playerGold >= roll && playerGold>=0 )
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



}
