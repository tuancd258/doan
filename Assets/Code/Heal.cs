using UnityEngine;

public class Heal : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

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
        Goldmetal.UndeadSurvivor.PoolManager.Instance.Despawn(gameObject);
    }
}
