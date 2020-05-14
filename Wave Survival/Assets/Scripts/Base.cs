﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour, Buildings
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
            Die();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHp -= damage;
    }

    public void Die()
    {
        //Lose
        Destroy(gameObject);
    }
}
