using Goldmetal.UndeadSurvivor;
using UnityEngine;

public class RangedWeaponController : WeaponController
{
    public Transform posWeapon;
    protected override void PerformAttack(Transform target)
    {
        Debug.Log("Chém vào cc");
        RangedWeaponData rangedData = weaponData as RangedWeaponData;
        if (rangedData == null) return;

        // Lấy đạn từ pool
        GameObject bullet = PoolManager.Instance.Get(rangedData.projectilePrefab);
        Vector3 pos = new Vector3(posWeapon.transform.position.x, posWeapon.transform.position.y, transform.position.z);
        // Reset vị trí & xoay
        bullet.transform.position = pos;
        //bullet.transform.rotation = Quaternion.identity;
        // Hướng bắn
        Vector3 dir = (target.position - pos).normalized;
        Bullet BulletScript = bullet.GetComponent<Bullet>();

        // Truyền lệnh "Fire" cùng với hướng và tốc độ LẤY TỪ WEAPONDATA
        if (BulletScript != null)
        {
            BulletScript.SetDirection(dir);
        }
    }

}