using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    [SerializeField] Transform contentParent;
    [SerializeField] GameObject characterButtonPrefab;
    public string namefle = "Weapon";
    void Start()
    {
        LoadAll();
    }

    void LoadAll()
    {
        Texture2D[] icons = Resources.LoadAll<Texture2D>(namefle);

        foreach (Texture2D icon in icons)
        {
            if (!icon.name.EndsWith("_icon")) continue;

            //Debug.Log("Tìm thấy icon: " + icon.name);
            string weaponName = icon.name.Replace("_icon", "");

            GameObject btnObj = Instantiate(characterButtonPrefab, contentParent);

            // Tạo sprite từ texture icon để hiển thị trong UI
            Sprite iconSprite = Sprite.Create(
                icon,
                new Rect(0, 0, icon.width, icon.height),
                new Vector2(0.5f, 0.5f)
            );

            btnObj.GetComponentInChildren<Image>().sprite = iconSprite;

            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnCharacterSelected(weaponName); // lưu Texture2D gốc
            });
        }
    }

    void OnCharacterSelected(string weaponName)
    {
        Debug.Log("Bạn đã chọn vũ khí: " + weaponName);
        WeaponData[] pres = Resources.LoadAll<WeaponData>("Weapon");
        foreach (var item in pres)
        {
            if (item.name.StartsWith(weaponName))
            {
                GameManager.Instance.SelectedWeapon = item;
                SceneManager.LoadScene("SampleScene");
                return;
            }   
        }

            Debug.Log("Không tìm thấy vũ khí");
            // Load scene mới
            SceneManager.LoadScene("SampleScene");
    }
}
