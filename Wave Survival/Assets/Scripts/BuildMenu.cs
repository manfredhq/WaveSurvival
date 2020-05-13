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
        Instantiate(basicWall, GameManager.instance.playerGO.transform.position + Vector3.down, Quaternion.identity, container.transform);
    }
}
