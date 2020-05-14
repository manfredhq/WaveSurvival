using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public GameObject basicWall;
    public GameObject container;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SelectBasicWall()
    {
        PlayerController player = GameManager.instance.playerGO.GetComponent<PlayerController>();
        player.agent.SetDestination(player.gameObject.transform.position);


        var basicWallGO = Instantiate(basicWall, GameManager.instance.playerGroundPosition, player.gameObject.transform.rotation, container.transform);
        basicWallGO.GetComponent<BuildPrefab>().player = player.gameObject;
        basicWallGO.GetComponent<BuildPrefab>().isBuildingMode = true;
    }
}
