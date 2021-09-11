using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : AIManager
{
    public float enemyScattering = 2.0f;
    public int enemiesCount = 10;

    public void Start()
    {
        this.SpawnEnemies(enemiesCount);
    }

    public void SpawnEnemies(int numberOfEnemies)
    {
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
}
