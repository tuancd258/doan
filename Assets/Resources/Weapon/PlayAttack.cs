using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance;
    public WeaponData startingWeaponsData;
    public int initialWeaponCount = 1;
    [SerializeField]
    private List<Vector3> weaponPositions = new List<Vector3>();
    [HideInInspector]
    public List<WeaponController> equippedWeapons = new List<WeaponController>();

    private Transform currentTarget;
    private float findTargetTimer = 0f;
    private const float FIND_TARGET_INTERVAL = 0.25f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        SpawnStartingWeapons();
        UpdateWeaponUI();
    }

    private void Update()
    {
        findTargetTimer -= Time.deltaTime;
        if (findTargetTimer <= 0f)
        {
            findTargetTimer = FIND_TARGET_INTERVAL;

            // ✅ SỬA LỖI: Lấy kết quả trả về và gán cho currentTarget
            GameObject nearestEnemyObject = FindNearestEnemy();
            if (nearestEnemyObject != null)
            {
                currentTarget = nearestEnemyObject.transform;
            }
            else
            {
                currentTarget = null;
            }
        }

        if (currentTarget != null)
        {
            Vector3 directionToTarget = (currentTarget.position - transform.position).normalized;
            foreach (WeaponController weapon in equippedWeapons)
            {
                if (weapon != null)
                {
                    weapon.SetTarget(directionToTarget);
                    weapon.Tick(currentTarget);
                }
            }
        }
    }

    GameObject FindNearestEnemy()
    {
        // Logic tìm kiếm của bạn đã rất tốt!
        List<GameObject> enemies = GameManager.Enemy; // Giả sử GameManager quản lý danh sách này
        GameObject nearestEnemy = null;
        float minSqrDistance = float.MaxValue;

        if (enemies == null || enemies.Count == 0) return null;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue; // Bỏ qua nếu enemy đã bị destroy

            float sqrDistance = (enemy.transform.position - transform.position).sqrMagnitude;
            if (sqrDistance < minSqrDistance)
            {
                minSqrDistance = sqrDistance;
                nearestEnemy = enemy;
            }
        }

        return nearestEnemy;
    }

    #region Các hàm quản lý vũ khí (Giữ nguyên)
    private void SpawnStartingWeapons()
    {
        for (int i = 0; i < initialWeaponCount; i++)
        {
            if (i >= weaponPositions.Count) break;
            if (startingWeaponsData == null)
            {
                Debug.LogError("Chưa gán Starting Weapons Data!");
                return;
            }
            WeaponData data = startingWeaponsData;
            if (GameManager.Instance != null)
            {
                data = GameManager.Instance.SelectedWeapon;
            }

            SpawnAndEquipWeapon(data, weaponPositions[i]);
        }
    }

    public void AddWeapon(WeaponData newWeapon)
    {
        if (equippedWeapons.Count >= weaponPositions.Count) return;
        int nextIndex = equippedWeapons.Count;
        Vector3 spawnPos = weaponPositions[nextIndex];
        SpawnAndEquipWeapon(newWeapon, spawnPos);
        UpdateWeaponUI();
    }

    private void SpawnAndEquipWeapon(WeaponData weaponData, Vector3 localPosition)
    {
        
      
        GameObject weaponObject = Instantiate(weaponData.weaponcontroller, transform.position + localPosition, Quaternion.identity, transform);
        WeaponController weaponInstance = weaponObject.GetComponent<WeaponController>();
        if (weaponInstance != null)
        {
            weaponInstance.Initialize(weaponData);
            equippedWeapons.Add(weaponInstance);
        }
        else
        {
            Debug.LogError($"Prefab của vũ khí '{weaponData.weaponName}' không chứa component WeaponController!");
        }
    }

    public void UpdateWeaponUI()
    {
        if (SlotItem.Instance != null)
        {
            SlotItem.Instance.UpdateWeaponSlots(equippedWeapons);
        }
    }
    #endregion
    // File: PlayerAttack.cs

    public void RemoveWeapon(int index)
    {
        // Kiểm tra an toàn để đảm bảo index hợp lệ
        if (index < 0 || index >= equippedWeapons.Count)
        {
            Debug.LogError("Index vũ khí muốn xóa không hợp lệ: " + index);
            return;
        }

        // 1. Tìm và HỦY GameObject của vũ khí trong Scene
        WeaponController weaponToDestroy = equippedWeapons[index];
        if (weaponToDestroy != null)
        {
            Destroy(weaponToDestroy.gameObject);
        }

        // 2. Xóa vũ khí khỏi danh sách
        equippedWeapons.RemoveAt(index);

        // 3. Quan trọng: Cập nhật lại UI để hiển thị sự thay đổi
        UpdateWeaponUI();
    }
}