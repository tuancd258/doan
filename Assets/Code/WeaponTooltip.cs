using UnityEngine;
using TMPro; 
using UnityEngine.UI;
using System.Text;

public class WeaponTooltip : MonoBehaviour
{
    // Singleton để dễ dàng truy cập
    public static WeaponTooltip Instance;

 
    public TextMeshProUGUI weaponNameText;
    public TextMeshProUGUI statsText;
     public Image weaponIcon; 

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        gameObject.SetActive(false);
    }

    public void ShowTooltip(WeaponData data)
    {
        weaponNameText.text = $"{data.weaponName} (Lv. {data.level})";
        // if (weaponIcon != null && data.weaponIcon != null) 
        // {
        //     weaponIcon.sprite = data.weaponIcon;
        // }

        StringBuilder statsBuilder = new StringBuilder();

        statsBuilder.AppendLine($"Damage: <color=yellow>{data.damage}</color>");
        float attackSpeed = 1f / data.cooldown;
        statsBuilder.AppendLine($"Attack Speed: <color=yellow>{attackSpeed:F2}</color>"); // F2: làm tròn 2 chữ số thập phân

        switch (data.weaponType)
        {
            case WeaponType.Melee:
                statsBuilder.AppendLine($"Attack Radius: <color=yellow>{data.attackRadius}</color>");
                statsBuilder.AppendLine($"Knockback: <color=yellow>{data.knockbackForce}</color>");
                break;

            case WeaponType.Ranged:
                statsBuilder.AppendLine($"Projectile Speed: <color=yellow>{data.projectileSpeed}</color>");
                break;
        }


        statsText.text = statsBuilder.ToString();

        gameObject.SetActive(true);
    }

    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }

    private void Update()
    {
        transform.position = Input.mousePosition;
    }
}