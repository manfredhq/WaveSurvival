using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildPrefab : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<Collider> colliders = new List<Collider>();
    public GameObject player;
    public float offset = 2f;
    public bool isBuild = false;
    public bool isBuildingMode = true;
    public bool isBuildable = true;

    private void Start()
    {
        if (isBuildingMode)
        {
            foreach (GameObject obj in objects)
            {
                obj.GetComponent<NavMeshObstacle>().enabled = false;
            }
            transform.position = GameManager.instance.playerGroundPosition + player.transform.localRotation * Vector3.forward * offset;
        }
    }
    private void Update()
    {
        if (isBuildingMode)
        {
            transform.rotation = player.transform.rotation;
            transform.position = GameManager.instance.playerGroundPosition + player.transform.localRotation * Vector3.forward * offset;
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Build();
            }
        }

    }

    public void Build()
    {
        isBuildingMode = false;
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<NavMeshObstacle>().enabled = true;
        }
    }
}
