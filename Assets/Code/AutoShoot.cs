using Goldmetal.UndeadSurvivor;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoShoot : MonoBehaviour
{
    public int bulletIndex = 0;      // Index của prefab trong PoolManager
    public float fireRate = 0.5f;    // Tốc độ bắn (0.5 giây/bắn 1 lần)
    private float fireCooldown = 0f;
    public Transform posWeapon;
    public SpriteRenderer weapon;
    public GameObject bullet1;
    void Update()
    {
        weapon.transform.position = transform.position + new Vector3(0.25f, -0.3f, -1);
        fireCooldown -= Time.deltaTime;
        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null)
        {
            Vector3 dir = (nearestEnemy.transform.position - transform.position).normalized;

            // Tính góc theo arctangent
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            // Gán rotation cho vũ khí
            weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            if (angle >= 90f || angle <= -90f)
            {
                weapon.transform.localScale = new Vector3(1,-1,1);
            }
            else
            {
                weapon.transform.localScale = new Vector3(1,1,1);
            }
                

            if (fireCooldown <= 0f)
            {
                Shoot(nearestEnemy.transform);
                fireCooldown = fireRate;
            }
            
        }
    }
    private void Start()
    {
        weapon.transform.position = transform.position + new Vector3(0.25f, -0.3f, -1);
    }
    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject nearest = enemies
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();

        return nearest;
    }

    void Shoot(Transform target)
    {
        // Lấy đạn từ pool
        GameObject bullet = PoolManager.Instance.Get(bullet1);
        Vector3 pos = new Vector3(posWeapon.transform.position.x, posWeapon.transform.position.y, transform.position.z);
        // Reset vị trí & xoay
        bullet.transform.position = pos;
        //bullet.transform.rotation = Quaternion.identity;

        // Hướng bắn
        Vector3 dir = (target.position - pos).normalized;
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}
