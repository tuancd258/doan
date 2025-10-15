using UnityEngine;
// --- THÊM DÒNG NÀY: Để sử dụng thư viện TextMeshPro ---
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public PlayerStarsData PlayerStarsData;

    public float currentHP;
    public float currentArmor;
    public float curentMoveSpeed;
    public int money;
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;
    
    public static PlayerStats instance;

    // --- THÊM DÒNG NÀY: Tạo một ô trong Inspector để kéo Text vào ---
    [Header("UI References")]
    public TextMeshProUGUI levelText;

    private void Awake()
    {
        instance = this;
    }

    // Thêm hàm Start để cập nhật UI ngay khi game bắt đầu
    void Start()
    {
        UpdateLevelText();
        LoadBase();
    }
    public void LoadBase()
    {
        currentHP=PlayerStarsData.maxHP;
        currentArmor=PlayerStarsData.armor;

    }
    public void AddExperience(int amount)
    {
        currentExp += amount;
        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }
    public float TakeDamage(float dmg)
    {
        float perDame;
        if (currentArmor > 0)
        {
            perDame = 1 / (1 + (currentArmor / 15));
        }else if (currentArmor < 0&&currentArmor >-15f)
        {
            perDame = (15-2*currentArmor) / (15-currentArmor);
        }
        else
        {
            perDame = 2;
        }
            float finalDmg = dmg*(1-perDame);
        if (Random.value < PlayerStarsData.dodgeChance)
        {
            finalDmg = 0;
        }
        currentHP-=finalDmg;
        if (currentHP < 0)
        {
            die();
        }
        return finalDmg;
    }
    public void die()
    {

    }
    public void AddMoney(int amount)
    {
        money += amount;
    }

    private void LevelUp()
    {
        currentExp -= expToNextLevel;
        currentLevel++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.5f);
        Debug.Log("LEVEL UP! Đạt cấp " + currentLevel);

        // --- THÊM DÒNG NÀY: Gọi hàm cập nhật UI mỗi khi lên cấp ---
        UpdateLevelText();  
    }

    // --- THÊM HÀM NÀY: Một hàm riêng để cập nhật Text ---
    void UpdateLevelText()
    {
        // Kiểm tra xem đã kết nối Text chưa để tránh lỗi
        if (levelText != null)
        {
            levelText.text = "Level: " + currentLevel;
        }
    }
}