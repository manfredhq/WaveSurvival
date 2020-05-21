using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobSpawnerManager : MonoBehaviour
{
    public List<GameObject> mobPrefabs = new List<GameObject>();
    public List<SpawnPoint> spawnPoints = new List<SpawnPoint>();
    public List<Wave> Waves = new List<Wave>();
    private List<SpawnPoint> currentWaveSpawnpoints = new List<SpawnPoint>();
    [HideInInspector]
    public int waveIndex = 0;
    public int maxWaveIndex = 10;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SpawnMobs()
    {
        List<int> randomNumber = new List<int>();
        currentWaveSpawnpoints.Clear();
        for (int i = 0; i < Waves[waveIndex].numberSpawn; i++)
        {
            int temp = Random.Range(0, spawnPoints.Count);
            if (randomNumber.Contains(temp))
            {
                i--;
            }
            else
            {
                randomNumber.Add(temp);
                currentWaveSpawnpoints.Add(spawnPoints[temp]);
            }
        }
        foreach (var spawnPoint in currentWaveSpawnpoints)
        {
            var temp = Random.Range(0, Waves[waveIndex].mobsPrefabs.Count);
            spawnPoint.mobPrefabs.Add(Waves[waveIndex].mobsPrefabs[temp]);
            StartCoroutine(spawnPoint.SpawnEnemies(3, 2));
        }
        waveIndex++;
    }
}

[System.Serializable]

public class Wave
{
    public int numberSpawn;
    public List<GameObject> mobsPrefabs = new List<GameObject>();
}