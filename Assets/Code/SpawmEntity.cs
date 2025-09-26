using Goldmetal.UndeadSurvivor;
using System.Collections;
using UnityEngine;

public class SpawmEntity : MonoBehaviour
{
    [SerializeField] private int enemyIndex = 0; // Index trong PoolManager.prefabs
    [SerializeField] private float minimunmSpawmTime = 1f;
    [SerializeField] private float maximumSpawmTime = 3f;
    [SerializeField] private Vector2 spawnAreaSize = new Vector2(5f, 5f);
    [SerializeField] private GameObject spawnEffectPrefab;
    [SerializeField] private GameObject enity;
    private float spawmTime;
    private PoolManager poolManager;

    void Awake()
    {
        poolManager = FindAnyObjectByType<PoolManager>(); // lấy PoolManager trong scene
        SetTimeUntilSpawm();
    }
    [SerializeField] private int enemiesPerSpawn = 3;
    void Update()
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
        // Spawn hiệu ứng trước
        if (spawnEffectPrefab != null)
        {
            GameObject effect = Instantiate(spawnEffectPrefab, position, Quaternion.identity);
            Destroy(effect, 2f);
        }

        // Chờ 2 giây mới spawn quái
        yield return new WaitForSeconds(2f);

        // Spawn quái từ pool
        GameObject enemy = PoolManager.Instance.Get(enity); // 1 là index enemy trong PoolManager.prefabs
        enemy.transform.position = position;
        enemy.transform.rotation = Quaternion.identity;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, spawnAreaSize);
    }
}
