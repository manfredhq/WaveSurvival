using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProdBuilding : MonoBehaviour
{

    public GameManager.currency prodType;
    public int prodAmount;
    public GameManager.Time timeProd;

    private bool haveProd = true;

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
    }
}
