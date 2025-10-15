using UnityEngine;

public class MenuController : MonoBehaviour
{
    // Kéo Panel Cài đặt của bạn vào ô này trong Inspector
    public GameObject settingsPanel;

    // Hàm này sẽ được gọi bởi nút "Settings"
    public void OpenSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
    }

    // Hàm này sẽ được gọi bởi nút "Back" hoặc "Close" bên trong Panel Cài đặt
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
    }
}