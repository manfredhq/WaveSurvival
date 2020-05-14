using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdBuilding : MonoBehaviour, Buildings
{

    public GameManager.currency prodType;
    public int prodAmount;
    public GameManager.Time timeProd;

    private bool haveProd = true;

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
        if (GameManager.instance.GetTime() == timeProd && !haveProd)
        {
            GameManager.instance.ResourcesGain(prodType, prodAmount);
            haveProd = true;
        }
        else if (GameManager.instance.GetTime() != timeProd && haveProd)
        {
            haveProd = false;
        }

        if (currentHp <= 0)
        {
            Die();
        }
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
