using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Settings")]
    public List<GameObject> slimeVariants;   // Wave 1–9 slimes
    public GameObject bossPrefab;            // Wave 10 boss
    public Transform spawnPoint;
    public float respawnDelay = 2f;

    [Header("Health Scaling")]
    public int baseEnemyHealth = 10;
    public int healthIncreasePerWave = 5;
    public float bossHealthMultiplier = 2.0f;

    [Header("Boss Settings")]
    public Vector3 bossSpawnOffset = new Vector3(0, 0, -1.5f);
    public float bossScaleMultiplier = 1.75f;

    private GameObject currentEnemy;

    private void Start()
    {
        SpawnEnemy();
    }

    public void SpawnEnemy()
    {
        int wave = WaveManager.Instance.currentWave;

        GameObject prefabToSpawn = (wave == 10)
            ? bossPrefab
            : slimeVariants[Random.Range(0, slimeVariants.Count)];

        // Calculate spawn position
        Vector3 spawnPosition = spawnPoint.position;
        if (wave == 10)
        {
            spawnPosition += bossSpawnOffset;
        }

        // Spawn enemy
        currentEnemy = Instantiate(prefabToSpawn, spawnPosition, spawnPoint.rotation);
        Debug.Log($"Wave {wave}: Spawned {currentEnemy.name}");

        // Assign health
        EnemyHealth health = currentEnemy.GetComponent<EnemyHealth>();
        if (health != null)
        {
            int calculatedHealth = baseEnemyHealth + ((wave - 1) * healthIncreasePerWave);
            if (wave == 10)
            {
                calculatedHealth = Mathf.RoundToInt(calculatedHealth * bossHealthMultiplier);
                currentEnemy.transform.localScale *= bossScaleMultiplier;
            }

            health.maxHealth = calculatedHealth;
            health.SetSpawner(this);
        }
        else
        {
            Debug.LogWarning("Spawned enemy has no EnemyHealth script.");
        }
    }

    public void NotifyEnemyDied()
    {
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
        WaveManager.Instance.AdvanceWave();
        SpawnEnemy();
    }
}
