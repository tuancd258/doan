using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Pause : MonoBehaviour
{
    private bool isPause = false;

    public Canvas canvas;
    public GameObject slotPrefab;       // Prefab có Image + Text
    public Transform panelContent;      // Panel chứa các slot

    public PlayerStarsData playerStarsData; // Tham chiếu dữ liệu player

    [System.Serializable]
    public class StarsUI
    {
        public string name;
        public Sprite icon;
        public string value;
    }

    void Start()
    {
        canvas.enabled = false;

        if (playerStarsData == null)
        {
            Debug.LogError("PlayerStarsData chưa gán!");
            return;
        }

        // Tạo danh sách slot
        StarsUI[] stars = new StarsUI[]
{
    new StarsUI { name="MaxHP",      icon=Resources.Load<Sprite>("Pause/max_hp"),      value=playerStarsData.maxHP.ToString() },
    new StarsUI { name="Armor",      icon=Resources.Load<Sprite>("Pause/armor"),       value=playerStarsData.armor.ToString() },
    new StarsUI { name="HealPerHit", icon=Resources.Load<Sprite>("Pause/hp_regeneration"),         value=playerStarsData.healPerHit.ToString() },
    new StarsUI { name="Dodge",      icon=Resources.Load<Sprite>("Pause/dodge"),       value=playerStarsData.dodgeChance.ToString() },
    new StarsUI { name="MoveSpeed",  icon=Resources.Load<Sprite>("Pause/speed"),   value=playerStarsData.moveSpeed.ToString() },
    new StarsUI { name="MeleeDamage",icon=Resources.Load<Sprite>("Pause/melee_damage"), value=playerStarsData.meleeDamage.ToString() },
    new StarsUI { name="RangedDamage",icon=Resources.Load<Sprite>("Pause/ranged_damage"),value=playerStarsData.rangedDamage.ToString() },
    new StarsUI { name="AttackSpeed",icon=Resources.Load<Sprite>("Pause/attack_speed"), value=playerStarsData.attackSpeed.ToString() },
    new StarsUI { name="DamageMultiplier",icon=Resources.Load<Sprite>("Pause/percent_damage"),value=playerStarsData.dameMultiplier.ToString() },
    new StarsUI { name="CriticalChance",icon=Resources.Load<Sprite>("Pause/crit_chance"), value=playerStarsData.criticalChance.ToString() },
    new StarsUI { name="Range",      icon=Resources.Load<Sprite>("Pause/range"),        value=playerStarsData.Range.ToString() },
    new StarsUI { name="LifeSteal",  icon=Resources.Load<Sprite>("Pause/lifesteal"),    value=playerStarsData.lifeSteal.ToString() }
};


        // Instantiate slot UI
        foreach (var stat in stars)
        {
            GameObject slot = Instantiate(slotPrefab, panelContent);
            slot.transform.localScale = Vector3.one;
            slot.transform.localPosition = Vector3.zero;
            Image iconImage = slot.transform.Find("Icon").GetComponent<Image>();
            TMP_Text valueText = slot.transform.Find("Text").GetComponent<TMP_Text>();

            if (iconImage != null) iconImage.sprite = stat.icon;
            if (valueText != null) valueText.text = stat.value;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPause = !isPause;
            Time.timeScale = isPause ? 0f : 1f;
            canvas.enabled = isPause;
        }
    }
}
