using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public GameObject enemyPrefab;
    public Transform spawnPoint;
    public float respawnDelay = 2f;

    private GameObject currentEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        if (enemyPrefab == null || spawnPoint == null)
        {
            Debug.LogError("EnemySpawner: Missing prefab or spawn point!");
            return;
        }

        currentEnemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        Debug.Log("Spawned enemy: " + currentEnemy.name);

        EnemyHealth health = currentEnemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            health.SetSpawner(this);
        }
        else
        {
            Debug.LogWarning("Spawned enemy has no EnemyHealth script.");
        }
    }

    public void NotifyEnemyDied()
    {
        // Immediately destroy the current enemy (optional here; already done in EnemyHealth)
        if (currentEnemy != null)
        {
            Destroy(currentEnemy);
            currentEnemy = null;
        }

        StartCoroutine(RespawnAfterDelay());
    }

    private IEnumerator RespawnAfterDelay()
    {
        yield return new WaitForSeconds(respawnDelay);
        SpawnEnemy();
    }
}
