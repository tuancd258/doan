using TMPro;
using UnityEngine;

public class FPS : MonoBehaviour
{
    public TMP_Text TextMeshPro;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float deltaTime = 0f;

    // Update is called once per frame
    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime-deltaTime)*0.001f;
        float fps=1f/deltaTime;
        if (TextMeshPro != null)
        {
            TextMeshPro.text = Mathf.Ceil(fps).ToString() + "FPS";
        }
        
    }
}
