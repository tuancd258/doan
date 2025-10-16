using Goldmetal.UndeadSurvivor;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject tien;
    public PoolManager pool;

    private bool barShown = false; // kiểm tra bar đã hiển thị chưa

    private void Start()
    {
        pool = PoolManager.Instance;



    }

    void OnEnable()
    {
        ResetHealth(); // mỗi lần spawn lại thì reset máu
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);


        if (currentHealth <= 0)
            Die();
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;

        // Ẩn bar khi spawn lại

        barShown = false;
    }

    private void Die()
    {
        // Rơi coin
        GameObject coin = PoolManager.Instance.Get(tien);
        coin.transform.position = transform.position;
        GameManager.addCoin(coin);

        GameManager.delEnemy(gameObject);

        // Tái chế quái
        Goldmetal.UndeadSurvivor.PoolManager.Instance.Despawn(gameObject);
    }
}
