using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Weapon : MonoBehaviour
{
    [SerializeField] Transform contentParent;
    [SerializeField] GameObject weaponButtonPrefab;
    void Start()
    {
        LoadAllweapons();
    }

    void LoadAllweapons()
    {
        Texture2D[] icons = Resources.LoadAll<Texture2D>("Weapons");

        foreach (Texture2D icon in icons)
        {
            if (!icon.name.EndsWith("_icon")) continue;

            Debug.Log("Tìm thấy icon: " + icon.name);
            string weaponName = icon.name.Replace("_icon", "");

            GameObject btnObj = Instantiate(weaponButtonPrefab, contentParent);

            // Tạo sprite từ texture icon để hiển thị trong UI
            Sprite iconSprite = Sprite.Create(
                icon,
                new Rect(0, 0, icon.width, icon.height),
                new Vector2(0.5f, 0.5f)
            );

            btnObj.GetComponentInChildren<Image>().sprite = iconSprite;

            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                foreach (Texture2D icon1 in icons)
                {
                    if (icon1.name.Equals(weaponName))
                    {
                        OnweaponSelected(weaponName, icon1); // lưu Texture2D gốc
                    }
                }

                    
            });
        }
    }

    void OnweaponSelected(string weaponName, Texture2D weaponTexture)
    {
        Debug.Log("Bạn đã chọn nhân vật: " + weaponName);

        GameManager.Instance.SelectedWeapon = weaponName;
        GameManager.Instance.SelectedWeaponTexture = weaponTexture;

        // Load scene mới
        SceneManager.LoadScene("SampleScene");
    }
}
