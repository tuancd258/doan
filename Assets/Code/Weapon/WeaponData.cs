using UnityEngine;

[CreateAssetMenu(menuName = "CREATER Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int level = 1;
    public GameObject weaponcontroller;
    //public GameObject weaponPrefab;
    public float damage=10f;
    public float cooldown=1f;
    public Sprite icon;
    public int price;

    // Cho ranged
    public GameObject projectilePrefab;
    public float projectileSpeed;

    // Cho melee
    public float attackRadius=1f;
    public float knockbackForce;

    public WeaponType weaponType; // enum để phân loại
}

public enum WeaponType
{
    Melee,
    Ranged
}