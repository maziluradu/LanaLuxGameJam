using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Enemy enemyPrefab;
    public Transform parent;
    public float interval = 1f;

    private float timer = 0;

    public void Update()
    {
        timer += Time.deltaTime;

        if (timer > interval)
        {
            timer = 0;
            var instance = Instantiate(enemyPrefab, transform.position, transform.rotation, parent);
        }
    }
}
