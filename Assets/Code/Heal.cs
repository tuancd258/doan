using UnityEngine;

public class Heal : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    public GameObject tien;
    void OnEnable()
    {
        ResetHealth(); // mỗi lần spawn lại thì reset máu
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        Debug.Log(gameObject.name + " trúng đạn, máu còn: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
    }

    private void Die()
    {
        Debug.Log(gameObject.name + " đã chết!");
        Instantiate(tien,transform.position,Quaternion.identity);
        GameManager.delEnemy(gameObject);
        Goldmetal.UndeadSurvivor.PoolManager.Instance.Despawn(gameObject);
    }
}