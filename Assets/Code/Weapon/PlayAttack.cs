using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public static PlayerAttack Instance;

    public WeaponData startingWeaponsData;
    public int initialWeaponCount = 1;

    [HideInInspector]
    public List<WeaponController> equippedWeapons = new List<WeaponController>();

    private List<Vector3> weaponPositions = new List<Vector3>();
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
        // Cập nhật vị trí vũ khí theo số lượng ban đầu
        UpdateWeaponPositions(initialWeaponCount);
        SpawnStartingWeapons();
        UpdateWeaponUI();
    }

    private void Update()
    {
        findTargetTimer -= Time.deltaTime;
        if (findTargetTimer <= 0f)
        {
            findTargetTimer = FIND_TARGET_INTERVAL;
            currentTarget = FindNearestEnemy()?.transform;
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

    // ------------------- Quản lý vị trí vũ khí -------------------
    private void UpdateWeaponPositions(int number)
    {
        weaponPositions.Clear();

        switch (number)
        {
            case 1:
                weaponPositions.Add(new Vector3(0.25f, -0.3f, -1));
                break;
            case 2:
                weaponPositions.Add(new Vector3(0.6f, -0.2f, -1));
                weaponPositions.Add(new Vector3(-0.3f, -0.2f, -1));
                break;
            case 3:
                weaponPositions.Add(new Vector3(0.6f, -0.2f, -1));
                weaponPositions.Add(new Vector3(-0.3f, -0.2f, -1));
                weaponPositions.Add(new Vector3(0.2f, 0.6f, -1));
                break;
            case 4:
                weaponPositions.Add(new Vector3(0.6f, -0.2f, -1));
                weaponPositions.Add(new Vector3(-0.3f, -0.2f, -1));
                weaponPositions.Add(new Vector3(0.6f, 0.5f, -1));
                weaponPositions.Add(new Vector3(-0.3f, 0.5f, -1));
                break;
            case 5:
                weaponPositions.Add(new Vector3(0.6f, -0.2f, -1));
                weaponPositions.Add(new Vector3(-0.3f, -0.2f, -1));
                weaponPositions.Add(new Vector3(0.8f, 0.35f, -1));
                weaponPositions.Add(new Vector3(-0.5f, 0.35f, -1));
                weaponPositions.Add(new Vector3(0.2f, 0.7f, -1));
                break;
            case 6:
                weaponPositions.Add(new Vector3(0.6f, -0.2f, -1));
                weaponPositions.Add(new Vector3(-0.3f, -0.2f, -1));
                weaponPositions.Add(new Vector3(0.8f, 0.3f, -1));
                weaponPositions.Add(new Vector3(-0.5f, 0.3f, -1));
                weaponPositions.Add(new Vector3(0.6f, 0.7f, -1));
                weaponPositions.Add(new Vector3(-0.3f, 0.7f, -1));
                break;
            default:
                Debug.LogWarning("Số lượng vũ khí vượt quá vị trí được định nghĩa!");
                break;
        }
    }

    // ------------------- Spawn vũ khí -------------------
    private void SpawnStartingWeapons()
    {
        for (int i = 0; i < initialWeaponCount; i++)
        {
            if (i >= weaponPositions.Count) break;
            SpawnAndEquipWeapon(startingWeaponsData, weaponPositions[i]);
        }
    }

    public void AddWeapon(WeaponData newWeapon)
    {
        int nextIndex = equippedWeapons.Count;
        UpdateWeaponPositions(nextIndex + 1); // cập nhật vị trí mới
        if (nextIndex < weaponPositions.Count)
        {
            SpawnAndEquipWeapon(newWeapon, weaponPositions[nextIndex]);
            UpdateWeaponUI();
        }
        else
        {
            Debug.LogWarning("Không thể thêm vũ khí, vượt quá số vị trí!");
        }
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
            Debug.LogError($"Prefab vũ khí '{weaponData.weaponName}' không chứa WeaponController!");
        }
    }

    public void RemoveWeapon(int index)
    {
        if (index < 0 || index >= equippedWeapons.Count)
        {
            Debug.LogError("Index vũ khí không hợp lệ: " + index);
            return;
        }

        WeaponController weaponToDestroy = equippedWeapons[index];
        if (weaponToDestroy != null) Destroy(weaponToDestroy.gameObject);

        equippedWeapons.RemoveAt(index);
        UpdateWeaponUI();

        // Cập nhật lại vị trí vũ khí còn lại
        UpdateWeaponPositions(equippedWeapons.Count);
        for (int i = 0; i < equippedWeapons.Count; i++)
        {
            equippedWeapons[i].transform.localPosition = weaponPositions[i];
        }
    }

    // ------------------- Tìm enemy gần nhất -------------------
    GameObject FindNearestEnemy()
    {
        List<GameObject> enemies = GameManager.Enemy;
        if (enemies == null || enemies.Count == 0) return null;

        GameObject nearest = null;
        float minSqrDistance = float.MaxValue;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue;
            float sqrDist = (enemy.transform.position - transform.position).sqrMagnitude;
            if (sqrDist < minSqrDistance)
            {
                minSqrDistance = sqrDist;
                nearest = enemy;
            }
        }
        return nearest;
    }

    // ------------------- Cập nhật UI -------------------
    public void UpdateWeaponUI()
    {
        if (SlotItem.Instance != null)
        {
            SlotItem.Instance.UpdateWeaponSlots(equippedWeapons);
        }
    }
}
