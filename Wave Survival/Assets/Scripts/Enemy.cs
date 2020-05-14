using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public int maxHp = 100;
    private int currentHp;

    public GameObject baseTarget;
    public int damage = 10;
    public float speed = 10f;
    public float range;
    public NavMeshAgent agent;

    public float attackBySecond = 1f;
    public int rangeDetectBuilding = 10;
    public GameObject buildTarget;

    private bool isAtacking = false;
    private Base baseToTarget;
    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxHp;
        baseToTarget = baseTarget.GetComponent<Base>();
        buildTarget = baseTarget;
        foreach (GameObject obj in GameManager.instance.buildings)
        {
            if (Vector3.Distance(transform.position, buildTarget.transform.position) > Vector3.Distance(transform.position, obj.transform.position))
            {
                buildTarget = obj;
            }
        }
        agent.SetDestination(buildTarget.transform.position);
        agent.stoppingDistance = range - (range / 10);
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (!agent.pathPending)
        {
            Vector3 dir = baseTarget.transform.position - transform.position;
            RaycastHit[] rayHit;
            rayHit = Physics.RaycastAll(transform.position, dir);
            Debug.DrawRay(transform.position, dir, Color.blue);
            if (agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                List<RaycastHit> rayHits = rayHit.ToList();
                rayHits.RemoveAt(0);
                foreach (var hit in rayHits)
                {
                    if (hit.distance <= range && !isAtacking && agent.velocity == new Vector3(0, 0, 0))
                    {
                        isAtacking = true;
                        StartCoroutine(Attack(hit.collider.gameObject.GetComponent<Buildings>()));
                    }
                }
            }
            if (agent.pathStatus == NavMeshPathStatus.PathComplete && !isAtacking && agent.velocity == new Vector3(0, 0, 0) && agent.remainingDistance <= range)
            {
                isAtacking = true;
                StartCoroutine(Attack(buildTarget.GetComponent<Buildings>()));
            }
        }
        if (buildTarget == null)
        {
            buildTarget = baseTarget;
            foreach (GameObject obj in GameManager.instance.buildings)
            {
                if (Vector3.Distance(transform.position, buildTarget.transform.position) > Vector3.Distance(transform.position, obj.transform.position))
                {
                    buildTarget = obj;
                }
            }
            agent.SetDestination(buildTarget.transform.position);
        }
        if (currentHp <= 0)
        {
            Die();
        }
    }
    IEnumerator Attack(Buildings obj)
    {
        while (obj.currentHp > 0)
        {
            obj.TakeDamage(damage);
            yield return new WaitForSeconds(1f / attackBySecond);
        }
        isAtacking = false;
        StopAllCoroutines();
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
        Debug.Log(gameObject.name + ": " + currentHp + "/" + maxHp);
    }

    private void Die()
    {
        GameManager.instance.enemies.Remove(gameObject);
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, rangeDetectBuilding);
    }
}
