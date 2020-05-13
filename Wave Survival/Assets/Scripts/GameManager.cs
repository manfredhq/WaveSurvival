using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    public NavMeshSurface surface;
    // Start is called before the first frame update
    void Start()
    {
        CalculateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CalculateMesh()
    {
        surface.BuildNavMesh();
    }
}
