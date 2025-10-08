using UnityEngine;

[CreateAssetMenu(menuName = "CREATER Weapon")]
public class WeaponData : ScriptableObject
{
    public string weaponName;
    public int level = 1;
    public GameObject weaponcontroller;
    public float damage;
    public float cooldown = 1f;
    public int price;
    public Sprite icon;

    // Ranged
    public GameObject projectilePrefab;
    public float projectileSpeed;

    // Melee
    public float attackRadius = 1f;
    public float knockbackForce;

    public WeaponType weaponType;


    public Sprite GetIcon()
    {
        if (icon != null)
            return icon;

        if (weaponcontroller != null)
        {
            SpriteRenderer sr = weaponcontroller.GetComponent<SpriteRenderer>();
            if (sr != null)
                return sr.sprite;
        }

        return null;
    }
}

public enum WeaponType
{
    Melee,
    Ranged
}
