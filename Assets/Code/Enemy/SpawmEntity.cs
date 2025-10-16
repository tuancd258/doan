using Goldmetal.UndeadSurvivor;
using System.Collections;
using UnityEngine;

public class SpawmEntity : MonoBehaviour
{
    //[SerializeField] private int enemyIndex = 0; // Index của quái trong PoolManager
    [SerializeField] private float minimunmSpawmTime = 1f;
    [SerializeField] private float maximumSpawmTime = 3f;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(5f, 5f);
    [SerializeField] private GameObject spawnEffectPrefab;
    public static SpawmEntity instance;
    private float spawmTime;
    private PoolManager poolManager;
    [SerializeField] private GameObject enity;
    void Awake()

    {

        poolManager = PoolManager.Instance;
        SetTimeUntilSpawm();
     
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // giữ lại khi load scene
        }
        else
        {
            Destroy(gameObject); // tránh duplicate
        }
    }
    [SerializeField] private int enemiesPerSpawn = 3;
    void Update()
    {
        StartSpawm();
    }
    public void StartSpawm()
    {
        spawmTime -= Time.deltaTime;
        if (spawmTime <= 0)
        {
            for (int i = 0; i < enemiesPerSpawn; i++)
            {
                Vector2 spawnPos = GetRandomSpawnPos();
                StartCoroutine(SpawnWithEffect(spawnPos));
            }
            SetTimeUntilSpawm();
        }
    }

    private void SetTimeUntilSpawm()
    {
        spawmTime = Random.Range(minimunmSpawmTime, maximumSpawmTime);
    }

    private Vector2 GetRandomSpawnPos()
    {
        Vector2 randomPos = new Vector2(
            Random.Range(-spawnAreaSize.x / 2, spawnAreaSize.x / 2),
            Random.Range(-spawnAreaSize.y / 2, spawnAreaSize.y / 2)
        );

        return (Vector2)transform.position + randomPos;
    }

    private IEnumerator SpawnWithEffect(Vector2 position)
    {
        GameObject SpawnEffect = null;
        if (spawnEffectPrefab != null)
        {
            
            SpawnEffect = PoolManager.Instance.Get(spawnEffectPrefab);
            SpawnEffect.transform.position = position;
            SpawnEffect.transform.rotation = Quaternion.identity;
        }

        yield return new WaitForSeconds(2f);
        Goldmetal.UndeadSurvivor.PoolManager.Instance.Despawn(SpawnEffect);
        GameObject enemy = null;
        if (PoolManager.Instance != null)
        {
            enemy = PoolManager.Instance.Get(enity);
            GameManager.addEnemy(enemy);
            enemy.transform.position = position;
            enemy.transform.rotation = Quaternion.identity;
        }
        else
        {
            Debug.LogError("PoolManager is NULL!");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
    public void StopSpawm()
    {
        this.enabled = false;
    }
}
