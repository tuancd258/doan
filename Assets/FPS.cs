using UnityEngine;
using TMPro;

public class FPS : MonoBehaviour
{
    public TMP_Text fpsText;
    float deltaTime=0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.001f;
        float fps = 1.0f / deltaTime;
        if(fpsText != null)
        {
            fpsText.text =Mathf.Ceil(fps).ToString()+" FPS";
        }
    }
            
}
