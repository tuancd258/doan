// File: WeaponController.cs (L?p cha)
using UnityEngine;

public abstract class WeaponController : MonoBehaviour
{
    public WeaponData weaponData { get; private set; }
    protected float currentCooldown;

    public void Initialize(WeaponData data)
    {
        weaponData = data;
    }

    public void Tick(Transform target)
    {
        currentCooldown -= Time.deltaTime;
        if (currentCooldown <= 0 && target != null)
        {
            PerformAttack(target);
            currentCooldown = weaponData.cooldown;
        }
    }

    protected abstract void PerformAttack(Transform target);
}

// File: RangedWeaponController.cs (G?n lên Prefab v? khí t?m xa)

// File: MeleeWeaponController.cs (G?n lên Prefab v? khí c?n chi?n)
//public class MeleeWeaponController : WeaponController
//{
//    protected override void PerformAttack(Transform target)
//    {
//        MeleeWeaponData meleeData = weaponData as MeleeWeaponData;
//        if (meleeData == null) return;

//        Debug.Log("Chém vào " + target.name);
//        // Logic quét và gây sát th??ng trong bán kính
//    }
//}