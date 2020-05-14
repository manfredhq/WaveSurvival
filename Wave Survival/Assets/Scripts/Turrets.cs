using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrets : MonoBehaviour
{
    public int range = 10;
    public int damage = 10;
    public GameObject bullet;
    public float attackBySecond = 2f;

    private GameObject target;
    private bool isAtacking = false;

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
}
