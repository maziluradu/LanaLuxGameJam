using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public GameObject mainPlayer;
    public List<AIController> AIs = new List<AIController>();
    public List<GameObject> AIPrefabs = new List<GameObject>();
    public Transform SpawnPoint;

    public void KillAI(AIController ai)
    {
        ai.Kill();
    }

    public void DamageAI(AIController ai, float damage)
    {
        ai.Damage(damage);
    }

    protected virtual AIController SpawnAI(GameObject prefab)
    {
        return this.SpawnAI(prefab, this.SpawnPoint.transform.position, this.transform.rotation);
    }

    protected virtual AIController SpawnAI(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        AIController spawnedAI = Instantiate(prefab, position, rotation).GetComponent<AIController>();

        this.AIs.Add(spawnedAI);
        this.AssignAIListeners(spawnedAI);

        return spawnedAI;
    }

    protected void AssignAIListeners(AIController ai)
    {
        // TODO: Remove listeners (if they are not garbage-collected)

        ai.onAIDied.AddListener(
            () => {
                RemoveAI(ai);
            }
        );
    }

    public void RemoveAI(AIController ai)
    {
        if (this.AIs.Contains(ai))
        {
            this.AIs.Remove(ai);
        }
        Destroy(ai.gameObject);
    }
}
