using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{
    public PlayerStarsData PlayerStarsData;

    [Header("Stats")]
    public float maxHP;
    public float currentHP;
    public float currentArmor;
    public float moveSpeed = 5f;
    public int money;
    public int currentLevel = 1;
    public int currentExp = 0;
    public int expToNextLevel = 100;
    private float Rangmanet = 3f;
    [Header("UI")]
    public TextMeshProUGUI levelText;
    private bool canTakeDame = true;
    private float damecooldown = 1f;


    public HealthBar healthBar;
    private bool isDead = false;
    public void Start()
    {
        healthBar.SetHealthPercent(1);
    }
    private void Awake()
    {
        maxHP = PlayerStarsData.maxHP;
        currentHP=maxHP;
        currentArmor = PlayerStarsData.armor;
        UpdateLevelText();
    }
    public void Update() { 
        manet(); 
    }
    public void manet()
    {
        List<GameObject> coins = GameManager.Coin; for (int i = coins.Count - 1; i >= 0; i--)
        {
            GameObject coin = coins[i]; float distan = Vector3.Distance(coin.transform.position, transform.position); 
            //Debug.Log(distan);
            if (distan < Rangmanet) { coin.transform.position = Vector3.MoveTowards(coin.transform.position, transform.position,10*Time.deltaTime); } 
            if (distan < 0.1f) {
                GameManager.removeFormList(GameManager.Coin,coin); 
                Goldmetal.UndeadSurvivor.PoolManager.Instance.Despawn(coin); 
            } } }
    public void TakeDamage(float dmg)
    {
        if (isDead) return;
        if(!canTakeDame) return;
        float perDame=0;
        //if (currentArmor > 0)
        //{


        //    perDame = 1 / (1 + (currentArmor / 15));
        //else if (currentArmor < 0 && currentArmor > -15f)
        //        perDame = (15 - 2 * currentArmor) / (15 - currentArmor);
        //    else
        //        perDame = 2;
        //}
        float finalDmg = dmg * (1 - perDame);

        if (Random.value < PlayerStarsData.dodgeChance)
            finalDmg = 0;

        currentHP -= finalDmg;
        healthBar.SetHealthPercent(currentHP/maxHP);
        Debug.Log("Player bị " + finalDmg + " damage, HP còn: " + currentHP);

        DamegeCooldown();

        if (currentHP <= 0)
            Die();
    }
    private IEnumerator DamegeCooldown()
    {
        canTakeDame = false;
        yield return new WaitForSeconds(damecooldown);
        canTakeDame= true;
    }

    private void Die()
    {
        isDead = true;
        Time.timeScale = 0f;
        Debug.Log("Player chết!");
    }

    void OnGUI()
    {
        if (isDead)
        {
            float width = 200;
            float height = 60;
            Rect rect = new Rect((Screen.width - width) / 2, (Screen.height - height) / 2, width, height);

            if (GUI.Button(rect, "Chơi lại"))
            {
                Time.timeScale = 1f;
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
    }

    public void AddExperience(int amount)
    {
        currentExp += amount;
        while (currentExp >= expToNextLevel)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentExp -= expToNextLevel;
        currentLevel++;
        expToNextLevel = Mathf.RoundToInt(expToNextLevel * 1.5f);
        Debug.Log("LEVEL UP! Đạt cấp " + currentLevel);
        UpdateLevelText();
    }

    void UpdateLevelText()
    {
        if (levelText != null)
            levelText.text = "Level: " + currentLevel;
    }
}
