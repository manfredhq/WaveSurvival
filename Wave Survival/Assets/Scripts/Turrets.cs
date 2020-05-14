using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour, Buildings
{
    public int range = 10;
    public int damage = 10;
    public GameObject bullet;
    public float attackBySecond = 2f;

    private GameObject target;
    private bool isAtacking = false;

    public int maxHp { get => hpPool; set => hpPool = value; }
    [HideInInspector]
    public int currentHp { get => hpCurrent; set => hpCurrent = value; }

    public int hpPool = 100;
    public int hpCurrent;

    void Start()
    {
        hpCurrent = hpPool;
    }
    private void Update()
    {
        //setting up the target
        if (target == null)
        {
            StopAllCoroutines();
            isAtacking = false;
            foreach (GameObject obj in GameManager.instance.enemies)
            {
                if (Vector3.Distance(obj.transform.position, transform.position) < range)
                {
                    target = obj;
                    break;
                }
                else
                {
                    target = null;
                }
            }
        }

        if (target != null && !isAtacking)
        {

            StartCoroutine(Shoot());
        }
        if (currentHp <= 0)
        {
            Die();
        }
    }

    private IEnumerator Shoot()
    {
        while (target != null)
        {
            isAtacking = true;
            //TODO: create an arrow
            target.GetComponent<Enemy>().TakeDamage(damage);
            yield return new WaitForSeconds(1f / attackBySecond);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    public void Die()
    {
        GameManager.instance.buildings.Remove(gameObject);
        Destroy(gameObject);
    }
}
