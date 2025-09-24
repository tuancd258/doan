using Goldmetal.UndeadSurvivor;
using System.Linq;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    public int bulletIndex = 0;      // Index của prefab trong PoolManager
    public float fireRate = 0.5f;    // Tốc độ bắn (0.5 giây/bắn 1 lần)
    private float fireCooldown = 0f;

    void Update()
    {
        fireCooldown -= Time.deltaTime;

        GameObject nearestEnemy = FindNearestEnemy();
        if (nearestEnemy != null && fireCooldown <= 0f)
        {
            Shoot(nearestEnemy.transform);
            fireCooldown = fireRate;
        }
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
        GameObject bullet = PoolManager.Instance.Get(0);

        // Reset vị trí & xoay
        bullet.transform.position = transform.position;
        bullet.transform.rotation = Quaternion.identity;

        // Hướng bắn
        Vector3 dir = (target.position - transform.position).normalized;
        bullet.GetComponent<Bullet>().SetDirection(dir);
    }
}
