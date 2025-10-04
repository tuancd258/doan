using Goldmetal.UndeadSurvivor;
using UnityEngine;

public class Meele : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float damage = 10f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            Heal heal = other.GetComponent<Heal>();
            if (heal != null)
            {
                heal.TakeDamage(damage);
            }
        }
    }
}
