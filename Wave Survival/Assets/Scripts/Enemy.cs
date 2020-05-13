using System.Collections;
using System.Collections.Generic;
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
        agent.stoppingDistance = range;
        agent.speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        /*Debug.Log(agent.remainingDistance);
        Debug.Log(agent.velocity);
        Debug.Log(agent.hasPath);
        Debug.Log(agent.isPathStale);*/
        Debug.Log(agent.pathStatus);
        if (!agent.pathPending)
        {
            NavMeshHit hit;
            agent.Raycast(target.transform.position, out hit);
            Debug.Log(hit.distance);
            if (agent.remainingDistance < range && agent.destination != null && !isAtacking)
            {
                isAtacking = true;
                StartCoroutine(Attack());
            }
            if (hit.hit && agent.pathStatus == NavMeshPathStatus.PathPartial)
            {
                if (hit.distance <= agent.stoppingDistance)
                {

                    Debug.Log(hit.GetType());
                }
            }

        }
    }

    IEnumerator Attack()
    {
        baseToTarget.TakeDamage(damage);
        yield return new WaitForSeconds(1f / attackBySecond);
    }
}
