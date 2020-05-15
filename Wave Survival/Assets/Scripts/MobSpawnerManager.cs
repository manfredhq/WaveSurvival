using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnerManager : MonoBehaviour
{
    public List<GameObject> mobPrefabs = new List<GameObject>();
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    // Start is called before the first frame update
    void Start()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            spawnPoint.mobPrefabs.Add(mobPrefabs[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnMobs()
    {
        foreach (var spawnPoint in spawnPoints)
        {
            StartCoroutine(spawnPoint.SpawnEnemies(3, mobPrefabs[Random.Range(0, mobPrefabs.Count)], 2));
        }
    }
}
