using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BuildPrefab : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    public List<Collider> colliders = new List<Collider>();
    public Material canBuild;
    public Material cantBuild;
    public GameObject player;
    public float offset = 2f;
    public bool isBuild = false;
    public bool isBuildingMode = true;
    public bool isBuildable = true;

    private List<Material> oldMat = new List<Material>();
    private void Start()
    {
        if (isBuildingMode)
        {
            foreach (GameObject obj in objects)
            {
                oldMat.Add(obj.GetComponent<Renderer>().material);
                obj.GetComponent<Renderer>().material = canBuild;
                obj.GetComponent<NavMeshObstacle>().enabled = false;
            }
            transform.position = GameManager.instance.playerGroundPosition + player.transform.localRotation * Vector3.forward * offset;
        }
    }
    private void Update()
    {
        if (!isBuild)
        {
            if (isBuildingMode && isBuildable)
            {

                if (Input.GetKeyDown(KeyCode.Return))
                {
                    Build();
                }
            }
            if (isBuildingMode)
            {
                transform.rotation = player.transform.rotation;
                transform.position = GameManager.instance.playerGroundPosition + player.transform.localRotation * Vector3.forward * offset;
            }
        }
    }

    public void Build()
    {
        isBuildingMode = false;
        foreach (GameObject obj in objects)
        {
            obj.GetComponent<Renderer>().material = oldMat[0];
            obj.GetComponent<NavMeshObstacle>().enabled = true;
            oldMat.RemoveAt(0);
        }
        isBuild = true;
        GameManager.instance.buildings.Add(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (!isBuild)
        {
            if (other.gameObject.name == "Ground" || other.gameObject.name == GameManager.instance.playerGO.name) { return; }
            isBuildable = false;
            foreach (GameObject obj in objects)
            {
                obj.GetComponent<Renderer>().material = cantBuild;
            }


        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!isBuild)
        {
            isBuildable = true;
            foreach (GameObject obj in objects)
            {
                obj.GetComponent<Renderer>().material = canBuild;
            }
        }
    }
}
