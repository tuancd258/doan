using System.Collections.Generic;
using UnityEngine;

public class f : MonoBehaviour
{
    public Transform weaponHolder;   // chỗ để gắn vũ khí
    public List<GameObject> startWeapons;

    void Start()
    {
        foreach (var weaponPrefab in startWeapons)
        {
            GameObject newWeapon = Instantiate(weaponPrefab, weaponHolder);
            newWeapon.transform.localPosition = Vector3.zero;  // đặt đúng chỗ holder
            newWeapon.transform.localRotation = Quaternion.identity;
        }
    }
}
