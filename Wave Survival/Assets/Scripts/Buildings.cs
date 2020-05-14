using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Buildings
{
    int maxHp { get; set; }
    int currentHp { get; set; }

    void TakeDamage(int damage);

    void Die();
}
