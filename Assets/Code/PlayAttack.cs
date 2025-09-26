using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;


public class PlayerAttack : MonoBehaviour
{


    private Transform currentTarget;
    private float findTargetTimer = 0.2f;
    private float findTargetInterval = 0.2f;
    public WeaponController startingWeapons;
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
            WeaponController prefab = startingWeapons;
            WeaponData data = startingWeaponsData;

            // Instantiate prefab trên player
            WeaponController weaponInstance = Instantiate(prefab, (transform.position + pos[i]), Quaternion.identity, transform);

            // Gán WeaponData
            weaponInstance.Initialize(data);

            // Thêm vào danh sách equippedWeapons
            equippedWeapons.Add(weaponInstance);
        }
    }

    void Update()
    {


        GameObject nearestEnemy = FindNearestEnemy();


        if (nearestEnemy != null)
        {
            foreach (var weapon in equippedWeapons)
            {
                weapon.Tick(nearestEnemy.transform);
                Vector3 dir = (nearestEnemy.transform.position - transform.position).normalized;

                // Tính góc theo arctangent
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                // Gán rotation cho vũ khí
                weapon.transform.rotation = Quaternion.Euler(0, 0, angle);
                if (angle >= 90f || angle <= -90f)
                {
                    weapon.transform.localScale = new Vector3(1, -1, 1);
                }
                else
                {
                    weapon.transform.localScale = new Vector3(1, 1, 1);
                }
            }


        }
    }

    GameObject FindNearestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (enemies.Length == 0) return null;

        GameObject nearest = enemies
            .OrderBy(e => Vector3.Distance(transform.position, e.transform.position))
            .FirstOrDefault();

        return nearest;
    }
}