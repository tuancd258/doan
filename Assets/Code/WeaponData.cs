using UnityEngine;

public abstract class WeaponData : ScriptableObject
{
    public string weaponName;
    public int level = 1;
    public GameObject weaponPrefab;
    public float damage;
    public float cooldown;
    //public WeaponData upgradedVersion; // Quan trọng: Tham chiếu đến phiên bản nâng cấp
}

// File: RangedWeaponData.cs
[CreateAssetMenu(fileName = "New Ranged Weapon", menuName = "Game/Weapon/Ranged Weapon")]
public class RangedWeaponData : WeaponData
{
    public GameObject projectilePrefab; // Chỉ vũ khí tầm xa mới có đạn
    //public float projectileSpeed;
}

// File: MeleeWeaponData.cs
[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Game/Weapon/Melee Weapon")]
public class MeleeWeaponData : WeaponData
{
    public float attackRadius; // Chỉ vũ khí cận chiến mới có tầm đánh
    public float knockbackForce;
}