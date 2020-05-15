using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public List<GameObject> mobPrefabs = new List<GameObject>();
    public int rangeToSpawn;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator SpawnEnemies(int number, GameObject enemy, int enemyBySecond = 1)
    {
        while (number > 0)
        {
            Debug.Log("test");
            Vector3 offset = new Vector3(Random.Range(-rangeToSpawn, rangeToSpawn), 0, Random.Range(-rangeToSpawn, rangeToSpawn));
            var temp = Instantiate(enemy, transform.position + offset, Quaternion.identity);
            temp.GetComponent<Enemy>().baseTarget = GameManager.instance.playerBase;
            GameManager.instance.enemies.Add(temp);
            number--;
            yield return new WaitForSeconds(1f / enemyBySecond);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, rangeToSpawn);
    }
}
