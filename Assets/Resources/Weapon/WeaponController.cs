// File: WeaponController.cs (L?p cha)
using Goldmetal.UndeadSurvivor;
using JetBrains.Annotations;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public WeaponData weaponData { get; private set; }
    protected float currentCooldown;
    public bool isAttack=false;
    public Transform posWeapon;
    public BoxCollider2D capsuleCollider;
    public void Start()
    {
        capsuleCollider = GetComponent<BoxCollider2D>();
        capsuleCollider.enabled = false;
    }
    public void Initialize(WeaponData data)
    {
        weaponData = data;
    }
    public void SetTarget(Vector3 dir)
    {
        // Tính góc theo arctangent
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // Gán rotation cho vũ khí
        transform.rotation = Quaternion.Euler(0, 0, angle);
        if (angle >= 90f || angle <= -90f)
        {
            transform.localScale = new Vector3(1, -1, 1);
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
    public void Tick(Transform target)
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0 && target != null)
        {
            isAttack = true;
            PerformAttack(target);
            currentCooldown = weaponData.cooldown;
        }
    }

    public void PerformAttack(Transform target)
    {
        if (weaponData.weaponType == WeaponType.Ranged)
        {
            Debug.Log("Chém vào quái");
            if (weaponData == null) return;

            // Lấy đạn từ pool
            GameObject bullet = PoolManager.Instance.Get(weaponData.projectilePrefab);
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
        else // Melee
        {
            // ✅ Thêm đoạn kiểm tra này
            if (capsuleCollider == null)
            {
                Debug.LogError($"Vũ khí '{weaponData.weaponName}' thiếu BoxCollider2D!");
                return; // Dừng lại để không gây lỗi
            }

            capsuleCollider.enabled = true;
            StartCoroutine(MeleeAttackRoutine(target));
        }
    }
    private IEnumerator MeleeAttackRoutine(Transform target)
    {
        Vector3 Startpos=transform.localPosition;
        Vector3 dir=(target.position-transform.position).normalized;
        Vector3 Attackpos=Startpos+weaponData.attackRadius*dir;
        float t = 0;
        while (t < 1f) {
            t += Time.deltaTime*5f;
            transform.localPosition = Vector3.Lerp(Startpos, Attackpos, t);
            yield return null;
        }
        t = 0;
        while (t < 1f)
        {
            t += Time.deltaTime * 5f;
            transform.localPosition = Vector3.Lerp(Attackpos, Startpos, t);
            yield return null;
        }
        capsuleCollider.enabled=false;
        isAttack = false;
        transform.localPosition = Startpos;
    }
}
