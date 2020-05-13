using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, Buildings
{
    public int maxHp { get => hpPool; set => hpPool = value; }
    [HideInInspector]
    public int currentHp { get => hpCurrent; set => hpCurrent = value; }

    public int hpPool;
    public int hpCurrent;

    // Start is called before the first frame update
    void Start()
    {
        hpCurrent = hpPool;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }
}
