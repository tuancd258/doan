using System.Collections.Generic;
using UnityEngine;

public class IteamManeger : MonoBehaviour
{
    [SerializeField] private Transform weaponHolder;        // nơi gắn vũ khí
    [SerializeField] private List<GameObject> startWeapons; // danh sách vũ khí khởi tạo

    private List<GameObject> equippedWeapons = new List<GameObject>();

    void Start()
    {
        // Khi game bắt đầu → spawn tất cả vũ khí trong startWeapons
        foreach (GameObject weaponPrefab in startWeapons)
        {
            EquipWeapon(weaponPrefab);
        }
    }

    public void EquipWeapon(GameObject newWeaponPrefab)
    {
        GameObject weapon = Instantiate(newWeaponPrefab, weaponHolder.position, Quaternion.identity, weaponHolder);
        equippedWeapons.Add(weapon);
    }
}
