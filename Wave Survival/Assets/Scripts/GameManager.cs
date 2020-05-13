using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    public NavMeshSurface surface;
    public GameObject player;
    public GameObject playerBase;
    public Light sun;

    public Color nightSunColor;
    public Color daySunColor;

    private GameObject playerGO;
    private Time currentTime;
    public enum Time
    {
        Day,
        Night
    }
    // Start is called before the first frame update
    void Start()
    {
        playerGO = Instantiate(player, playerBase.transform.position + Vector3.back, Quaternion.identity);
        DayStarting();
        CalculateMesh();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeTime()
    {
        switch (currentTime)
        {
            case Time.Day:
                DayEnding();
                NightStarting();
                break;
            case Time.Night:
                NightEnding();
                DayStarting();
                break;
        }
    }
    public void CalculateMesh()
    {
        surface.BuildNavMesh();
    }

    public void DayStarting()
    {
        sun.color = daySunColor;
        currentTime = Time.Day;
    }

    public void DayEnding()
    {
        playerGO.SetActive(false);
    }

    public void NightStarting()
    {
        sun.color = nightSunColor;
        currentTime = Time.Night;
    }

    public void NightEnding()
    {
        playerGO.transform.position = playerBase.transform.position + Vector3.back;
        playerGO.SetActive(true);
    }
}
