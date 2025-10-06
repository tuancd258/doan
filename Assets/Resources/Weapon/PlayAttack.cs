using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerAttack : MonoBehaviour
{
    
    public WeaponData startingWeaponsData;
    private List<WeaponController> equippedWeapons = new List<WeaponController>();
    private List<Vector3> pos = new List<Vector3>();


    public int number = 1;
    void Start()
    {
        switch (number)
        {
            case 1:
                pos.Add(new Vector3(0.25f, -0.3f, -1));
                break;
            case 2:
                pos.Add(new Vector3(0.6f, -0.2f, -1));
                pos.Add(new Vector3(-0.3f, -0.2f, -1));
                break;
            case 3:
                pos.Add(new Vector3(0.6f, -0.2f, -1));
                pos.Add(new Vector3(-0.3f, -0.2f, -1));
                pos.Add(new Vector3(0.2f, 0.6f, -1));
                break;
            case 4:
                pos.Add(new Vector3(0.6f, -0.2f, -1));
                pos.Add(new Vector3(-0.3f, -0.2f, -1));
                pos.Add(new Vector3(0.6f, 0.5f, -1));
                pos.Add(new Vector3(-0.3f, 0.5f, -1));
                break;
            case 5:
                pos.Add(new Vector3(0.6f, -0.2f, -1));
                pos.Add(new Vector3(-0.3f, -0.2f, -1));
                pos.Add(new Vector3(0.8f, 0.35f, -1));
                pos.Add(new Vector3(-0.5f, 0.35f, -1));
                pos.Add(new Vector3(0.2f, 0.7f, -1));
                break;
            case 6:
                pos.Add(new Vector3(0.6f, -0.2f, -1));
                pos.Add(new Vector3(-0.3f, -0.2f, -1));
                pos.Add(new Vector3(0.8f, 0.3f, -1));
                pos.Add(new Vector3(-0.5f, 0.3f, -1));
                pos.Add(new Vector3(0.6f, 0.7f, -1));
                pos.Add(new Vector3(-0.3f, 0.7f, -1));
                break;
        }

        for (int i = 0; i < number; i++)
        {

            WeaponData data = startingWeaponsData;
            if (GameManager.Instance != null)
            {
                data = GameManager.Instance.SelectedWeapon;
            }
            GameObject prefab = Instantiate(data.weaponcontroller, (transform.position + pos[i]), Quaternion.identity, transform);
            WeaponController weaponInstance = prefab.GetComponent<WeaponController>();

            // Instantiate prefab trên player

            // Gán WeaponData
            if (weaponInstance != null)
            {
                weaponInstance.Initialize(data);

                // Thêm vào danh sách equippedWeapons
                equippedWeapons.Add(weaponInstance);
            }
            else
            {
                Debug.Log("an cuc");
            }

           
        }
    }

    void Update()
    {


        GameObject nearestEnemy = FindNearestEnemy();


        if (nearestEnemy != null)
        {
            foreach (var weapon in equippedWeapons)
            {
                if(weapon.isAttack) continue;
                Vector3 dir = (nearestEnemy.transform.position - transform.position).normalized;

               weapon.SetTarget(dir);
                weapon.Tick(nearestEnemy.transform);
            }


        }
    }

    GameObject FindNearestEnemy()
    {
        List<GameObject> enemies = GameManager.Enemy;
        GameObject e = null;
        float min=float.MaxValue;
        if (enemies == null) return null;

        foreach (var enemy in enemies) { 
            float dis=(enemy.transform.position-transform.position).sqrMagnitude;
            if (dis < min)
            {
                min = dis;
                e = enemy;
            }
        }

        return e;
    }
}