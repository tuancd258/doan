using UnityEngine;
using UnityEngine.UI;

public class UI_Manage : MonoBehaviour
{
    [SerializeField] Transform contentParent;
    [SerializeField] GameObject characterButtonPrefab;

    void Start()
    {
        LoadAllCharacters();
        
    }

    void LoadAllCharacters()
    {
        // Load tất cả icon trong Resources/Characters và các folder con
        Texture2D[] icons = Resources.LoadAll<Texture2D>("Characters");

        foreach (Texture2D icon in icons)
        {
            if (!icon.name.EndsWith("_icon")) continue;
            Debug.Log("Tìm thấy icon: " + icon.name);
            string characterName = icon.name.Replace("_icon", "");
            GameObject btnObj = Instantiate(characterButtonPrefab, contentParent);
            btnObj.GetComponentInChildren<UnityEngine.UI.Image>().sprite = Sprite.Create(
                icon,
                new Rect(0, 0, icon.width, icon.height),
                new Vector2(0.5f, 0.5f)
            );
            btnObj.GetComponent<Button>().onClick.AddListener(() =>
            {
                OnCharacterSelected(characterName);
            });
        }
    }

    void OnCharacterSelected(string characterName)
    {
        Debug.Log("Bạn đã chọn nhân vật: " + characterName);
        // TODO: lưu vào GameManager hoặc PlayerPrefs
    }
}
