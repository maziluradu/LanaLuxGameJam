using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    public GameObject mainPlayer;
    public List<AIController> AIs = new List<AIController>();
    public List<GameObject> AIPrefabs = new List<GameObject>();
    public Transform SpawnPoint;

    protected virtual AIController SpawnAI(GameObject prefab)
    {
        return SpawnAI(prefab, this.SpawnPoint.transform.position, this.transform.rotation);
    }

    protected virtual AIController SpawnAI(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        AIController spawnedAI = Instantiate(prefab, position, rotation).GetComponent<AIController>();

        AIs.Add(spawnedAI);
        AddListeners(spawnedAI);

        return spawnedAI;
    }

    protected void AddListeners(AIController ai)
    {
        ai.CombatUnit.OnDeath += HandleOnDeath;
    }

    protected virtual void HandleOnDeath(DeathEventData eventData)
    {
        var ai = eventData.victim.GetComponent<AIController>();
        if (ai == null)
        {
            Debug.LogError("AIManager: Death event for non-ai");
            return;
        }

        ai.CombatUnit.OnDeath -= HandleOnDeath;
        AIs.Remove(ai);
    }
}
