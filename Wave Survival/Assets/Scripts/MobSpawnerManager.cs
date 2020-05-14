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

    public void SpawnMobs(GameObject target)
    {
        foreach (var spawnPoint in spawnPoints)
        {
            var temp = Instantiate(spawnPoint.mobPrefabs[0], spawnPoint.transform.position, Quaternion.identity);
            temp.GetComponent<Enemy>().baseTarget = target;
            GameManager.instance.enemies.Add(temp);
        }
    }
}
