using UnityEngine;
using UnityEngine.SceneManagement;
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
        Texture2D[] icons = Resources.LoadAll<Texture2D>("Characters");

        foreach (Texture2D icon in icons)
        {
            if (!icon.name.EndsWith("_icon")) continue;

            Debug.Log("Tìm thấy icon: " + icon.name);
            string characterName = icon.name.Replace("_icon", "");

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
                OnCharacterSelected(characterName, icon); // lưu Texture2D gốc
            });
        }
    }

    void OnCharacterSelected(string characterName, Texture2D characterTexture)
    {
        Debug.Log("Bạn đã chọn nhân vật: " + characterName);

        GameManager.Instance.SelectedCharacter = characterName;
        GameManager.Instance.SelectedCharacterTexture = characterTexture;

        // Load scene mới
        SceneManager.LoadScene("SampleScene");
    }
}
