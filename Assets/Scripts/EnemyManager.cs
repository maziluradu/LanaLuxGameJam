using System;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : AIManager
{
    [Min(0)][Tooltip("Spawn enemies every x seconds. Use 0 to only spawn at start")]
    public float interval = 10f;
    public float enemyScattering = 2.0f;
    public int enemiesCount = 10;
    public float enemyMultiplierForEachWave = 1.2f;

    public UnityEvent onWaveEnded = new UnityEvent();
    public UnityEvent<string> onWaveStarted = new UnityEvent<string>();

    private float timer = 0f;
    private bool waveFrozen = false;
    private int currentWave = 0;

    public void FreezeWave(bool toggle)
    {
        if (this.AIs.Count == 0)
        {
            waveFrozen = toggle;
        }
    }

    public void Update()
    {
        if (!waveFrozen)
        {
            timer += Time.deltaTime;

            if (currentWave == 0 || interval > 0 && timer >= interval)
            {
                timer = 0;
                this.SpawnEnemies(Convert.ToInt32(enemiesCount * Mathf.Pow(enemyMultiplierForEachWave, currentWave++)));
            }
        }
    }

    public void SpawnEnemies(int numberOfEnemies)
    {
        FreezeWave(true);
        this.onWaveStarted.Invoke($"Wave {currentWave} has begun.");
        for (int i = 0; i < numberOfEnemies; i++)
        {
            SpawnRandomEnemy();
        }
    }

    public void SpawnRandomEnemy()
    {
        if (this.AIPrefabs.Count > 0)
        {
            this.SpawnAI(
                this.AIPrefabs[UnityEngine.Random.Range(0, this.AIPrefabs.Count)],
                this.SpawnPoint.transform.position + new Vector3(UnityEngine.Random.Range(0.0f, this.enemyScattering), 0, UnityEngine.Random.Range(0.0f, this.enemyScattering)),
                this.SpawnPoint.transform.rotation
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

        if (AIs.Count == 0)
        {
            onWaveEnded.Invoke();
        }
    }
}
