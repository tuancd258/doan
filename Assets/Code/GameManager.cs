using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public string SelectedCharacter;
    public Texture2D SelectedCharacterTexture;

    public string SelectedWeapon;
    public Texture2D SelectedWeaponTexture;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // không bị phá khi load scene mới
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
