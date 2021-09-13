using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : AIManager
{
    [Min(0)][Tooltip("Spawn enemies every x seconds. Use 0 to only spawn at start")]
    public float interval = 10f;
    public float enemyScattering = 2.0f;
    public int enemiesCount = 10;
    [Range(0.0f, 1.0f)]
    public float burstRatio = 0.3f;
    public float enemyMultiplierForEachWave = 1.2f;

    public UnityEvent<int> onWaveEnded = new UnityEvent<int>();
    public UnityEvent<string> onWaveStarted = new UnityEvent<string>();

    private float timer = 0f;
    private bool waveFrozen = true;
    public int currentWave = 1;
    public int enemiesSpawnedThisWave = 0;

    public void FreezeWave(bool toggle)
    {
        if (this.AIs.Count == 0)
        {
            waveFrozen = toggle;
        }
    }

    public void Update()
    {
        var enemiesToSpawn = GetEnemiesToSpawn();

        if (!waveFrozen || this.enemiesSpawnedThisWave > 0 && this.enemiesSpawnedThisWave < enemiesToSpawn)
        {
            timer += Time.deltaTime;

            if (currentWave == 0 || interval > 0 && timer >= interval)
            {
                if (this.enemiesSpawnedThisWave < enemiesToSpawn)
                {
                    timer = Mathf.Max(0, (1.0f - burstRatio) * interval);
                    this.SpawnEnemies(Convert.ToInt32(Math.Min(enemiesToSpawn * burstRatio, enemiesToSpawn - enemiesSpawnedThisWave)));
                } else
                {
                    timer = 0;
                }
            }
        }
    }

    public void SpawnEnemies(int numberOfEnemies)
    {
        FreezeWave(true);
        if (this.enemiesSpawnedThisWave == 0)
        {
            this.onWaveStarted.Invoke($"Wave {currentWave} has begun.");
        }
        for (int i = 0; i < numberOfEnemies; i++)
        {
            this.enemiesSpawnedThisWave++;
            SpawnRandomEnemy();
        }
    }

    public void SpawnRandomEnemy()
    {
        if (this.AIPrefabs.Count > 0)
        {
            var randomSpawnPoint = this.SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Count)];
            this.SpawnAI(
                this.AIPrefabs[UnityEngine.Random.Range(0, this.AIPrefabs.Count)],
                randomSpawnPoint.transform.position + new Vector3(UnityEngine.Random.Range(0.0f, this.enemyScattering), 0, UnityEngine.Random.Range(0.0f, this.enemyScattering)),
                randomSpawnPoint.transform.rotation
            );
        }
    }

    protected override AIController SpawnAI(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        AIController spawnedAI = base.SpawnAI(prefab, position, rotation);
        spawnedAI.target = mainPlayer;

        return spawnedAI;
    }

    protected override void HandleOnDeath(DeathEventData eventData)
    {
        base.HandleOnDeath(eventData);

        if (AIs.Count == 0 && this.enemiesSpawnedThisWave >= GetEnemiesToSpawn())
        {
            onWaveEnded.Invoke(currentWave);
            enemiesSpawnedThisWave = 0;
            timer = 0;
            currentWave++;
        }
    }

    private int GetEnemiesToSpawn()
    {
        return Convert.ToInt32(enemiesCount * Mathf.Pow(enemyMultiplierForEachWave, currentWave + 1));
    }
}
