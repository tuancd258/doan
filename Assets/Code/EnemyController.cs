using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Rewards")]
    public int expValue = 10;
    public int moneyValue = 5;

    private PlayerStats playerStats;

    void Awake()
    {

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
            playerStats = playerObject.GetComponent<PlayerStats>();
    }

    
    public void Die()
    {
        if (playerStats != null)
        {
            playerStats.AddExperience(expValue);
            playerStats.AddMoney(moneyValue);
        }
        
        gameObject.SetActive(false);
    }
}