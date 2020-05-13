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
    public MobSpawnerManager spawnerManager;

    public Color nightSunColor;
    public Color daySunColor;

    public GameObject playerGO;
    private Time currentTime;
    public enum Time
    {
        Day,
        Night
    }

    public static GameManager instance;

    //to handle the singleton
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("too many Game manager");
            return;
        }
        instance = this;

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
        spawnerManager.SpawnMobs(playerBase);
        sun.color = nightSunColor;
        currentTime = Time.Night;
    }

    public void NightEnding()
    {
        playerGO.transform.position = playerBase.transform.position + Vector3.back;
        playerGO.SetActive(true);
    }
}
