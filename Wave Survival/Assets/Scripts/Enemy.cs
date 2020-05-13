using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{

    public GameObject target;
    public int damage = 10;
    public float speed = 10f;
    public float range;
    public NavMeshAgent agent;

    public float attackBySecond = 1f;

    private bool isAtacking = false;
    private Base baseToTarget;
    // Start is called before the first frame update
    void Start()
    {
        baseToTarget = target.GetComponent<Base>();
        agent.SetDestination(target.transform.position);
        agent.stoppingDistance = range - (range / 10);
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(agent.remainingDistance);
        Debug.Log(agent.velocity);
        Debug.Log(agent.hasPath);
        Debug.Log(agent.isPathStale);*/
        if (!agent.pathPending)
        {

            Vector3 dir = target.transform.position - transform.position;
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
            if (agent.pathStatus == NavMeshPathStatus.PathComplete && !isAtacking && agent.velocity == new Vector3(0, 0, 0))
            {
                isAtacking = true;
                StartCoroutine(Attack(target.GetComponent<Buildings>()));
            }
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
    }
}
