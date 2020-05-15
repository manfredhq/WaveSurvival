using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildMenu : MonoBehaviour
{
    public ShopBlueprint basicWall;
    public ShopBlueprint basicTurret;
    public ShopBlueprint farm;
    public ShopBlueprint lumber;
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
        if (GameManager.instance.currentBuildPrefab == null)
        {
            if (GameManager.instance.BuyBuilding(basicWall))
            {
                var basicWallGO = Instantiate(basicWall.prefab, GameManager.instance.playerGroundPosition, player.gameObject.transform.rotation, container.transform);
                basicWallGO.GetComponent<BuildPrefab>().player = player.gameObject;
                basicWallGO.GetComponent<BuildPrefab>().isBuildingMode = true;
                GameManager.instance.currentBuildPrefab = basicWallGO;
            }
        }
    }

    public void SelectBasicTurret()
    {
        PlayerController player = GameManager.instance.playerGO.GetComponent<PlayerController>();
        player.agent.SetDestination(player.gameObject.transform.position);
        if (GameManager.instance.currentBuildPrefab == null)
        {
            if (GameManager.instance.BuyBuilding(basicTurret))
            {
                var basicWallGO = Instantiate(basicTurret.prefab, GameManager.instance.playerGroundPosition, player.gameObject.transform.rotation, container.transform);
                basicWallGO.GetComponent<BuildPrefab>().player = player.gameObject;
                basicWallGO.GetComponent<BuildPrefab>().isBuildingMode = true;
                GameManager.instance.currentBuildPrefab = basicWallGO;

            }
        }
    }

    public void SelectFarm()
    {

        PlayerController player = GameManager.instance.playerGO.GetComponent<PlayerController>();
        player.agent.SetDestination(player.gameObject.transform.position);
        if (GameManager.instance.currentBuildPrefab == null)
        {
            if (GameManager.instance.BuyBuilding(farm))
            {

                var basicWallGO = Instantiate(farm.prefab, GameManager.instance.playerGroundPosition, player.gameObject.transform.rotation, container.transform);
                basicWallGO.GetComponent<BuildPrefab>().player = player.gameObject;
                basicWallGO.GetComponent<BuildPrefab>().isBuildingMode = true;
                GameManager.instance.currentBuildPrefab = basicWallGO;
            }
        }
    }

    public void SelectLumber()
    {

        PlayerController player = GameManager.instance.playerGO.GetComponent<PlayerController>();
        player.agent.SetDestination(player.gameObject.transform.position);
        if (GameManager.instance.currentBuildPrefab == null)
        {
            if (GameManager.instance.BuyBuilding(lumber))
            {

                var basicWallGO = Instantiate(lumber.prefab, GameManager.instance.playerGroundPosition, player.gameObject.transform.rotation, container.transform);
                basicWallGO.GetComponent<BuildPrefab>().player = player.gameObject;
                basicWallGO.GetComponent<BuildPrefab>().isBuildingMode = true;
                GameManager.instance.currentBuildPrefab = basicWallGO;
            }
        }
    }
}

[System.Serializable]
public class ShopBlueprint
{
    public CostBlueprint[] costs;
    public GameObject prefab;
}
[System.Serializable]
public class CostBlueprint
{
    public GameManager.currency currencyType;
    public int cost;
}