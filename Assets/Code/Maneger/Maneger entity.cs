using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Manegerentity : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timetext;
    [SerializeField] TextMeshProUGUI waveText;
    public static Manegerentity instance;

    bool waveRunning = true;
    int currentWave = 0;
    int currentTime;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    public bool WaveRunning { get { return waveRunning; } }
    private void Start()
    {
       StartFirstWave();
    }
    private void Update()
    {
       
    }   

    private void StartFirstWave()
    {
        StopAllCoroutines();
        currentTime = 1;

        timetext.color = Color.white;
        currentWave = 1;   // KHỞI TẠO wave 1
        waveRunning = true;
        currentTime = 2;

        waveText.text = "Wave: " + currentWave;
        StartCoroutine(WaveTime());

        if (SpawmEntity.instance != null)
            SpawmEntity.instance.StartSpawm();
    }

    IEnumerator WaveTime()
    {
        while (waveRunning && currentTime > 0)
        {
            yield return new WaitForSeconds(1f);
            currentTime--;

            timetext.text = currentTime.ToString();

            if (currentTime <= 0)
                WaveComplete();
        }
    }

    private void WaveComplete()
    {
        StopAllCoroutines();
        waveRunning = false;

        timetext.text = "0";
        timetext.color = Color.green;
        Time.timeScale = 0f;
        SpawmEntity.instance.StopSpawm();
        SceneManager.LoadScene("ShopTest", LoadSceneMode.Additive);

    }
    IEnumerator RunWave()
    {
        while (waveRunning)
        {
            yield return null;
        }
    }

    public void StartNextWave()
    {
        StopAllCoroutines();  // dừng timer cũ

        waveRunning = true;
        currentWave++;        // chỉ TĂNG WAVE ở đây
        currentTime = 30;

        waveText.text = "Wave: " + currentWave;
        timetext.color = Color.white;
        timetext.text = currentTime.ToString();

        StartCoroutine(WaveTime());

        if (SpawmEntity.instance != null)
        {
            SpawmEntity.instance.enabled = true;
            SpawmEntity.instance.StartSpawm();
        }
        else
        {
            Debug.LogError("SpawmEntity.instance is NULL!");
        }
    }


}
