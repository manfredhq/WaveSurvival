using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{

    public NavMeshSurface surface;
    public List<GameObject> enemies = new List<GameObject>();
    public List<GameObject> buildings = new List<GameObject>();
    public GameObject player;
    public GameObject playerBase;
    public Light sun;
    public MobSpawnerManager spawnerManager;

    public Color nightSunColor;
    public Color daySunColor;

    #region currency
    public enum currency
    {
        Wood,
        Food,
        Gold,
        Stone,
        Pop
    }
    private int gold = 100;
    private int food = 100;
    private int wood = 100;
    private int stone = 100;
    private int maxPop = 5;
    private int currentPop;

    #endregion

    public GameObject playerGO;
    public Vector3 playerGroundPosition;
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
        currentPop = maxPop;
        playerGO = Instantiate(player, playerBase.transform.position + Vector3.back, Quaternion.identity);
        playerGroundPosition = new Vector3(playerGO.transform.position.x, 0, playerGO.transform.position.z);
        DayStarting();
        CalculateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        playerGroundPosition = new Vector3(playerGO.transform.position.x, 0, playerGO.transform.position.z);

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
        spawnerManager.SpawnMobs();
        sun.color = nightSunColor;
        currentTime = Time.Night;
    }

    public void NightEnding()
    {
        playerGO.transform.position = playerBase.transform.position + Vector3.back;
        playerGO.SetActive(true);
    }

    public Time GetTime()
    {
        return currentTime;
    }

    public void ResourcesGain(currency type, int amount)
    {
        switch (type)
        {
            case currency.Wood:
                wood += amount;
                break;
            case currency.Food:
                food += amount;
                break;
            case currency.Gold:
                gold += amount;
                break;
            case currency.Stone:
                stone += amount;
                break;
            case currency.Pop:
                currentPop += amount;
                maxPop += amount;
                break;
        }
        Debug.Log("Stone : " + stone);
        Debug.Log(" Wood : " + wood);
        Debug.Log("Food : " + food);
        Debug.Log("Gold : " + gold);
        Debug.Log("Pop : " + currentPop + "/" + maxPop);
    }

    public bool BuyBuilding(ShopBlueprint blueprint)
    {
        foreach (CostBlueprint cost in blueprint.costs)
        {
            if (!CheckEnoughCurrency(cost.currencyType, cost.cost))
            {
                return false;
            }
        }
        foreach (CostBlueprint cost in blueprint.costs)
        {
            RemoveCurrency(cost.currencyType, cost.cost);
        }
        return true;

    }

    public bool CheckEnoughCurrency(currency type, int amount)
    {
        switch (type)
        {
            case currency.Wood:
                if (amount > wood) { return false; }
                break;
            case currency.Food:
                if (amount > food) { return false; }
                break;
            case currency.Gold:
                if (amount > gold) { return false; }
                break;
            case currency.Stone:
                if (amount > stone) { return false; }
                break;
            case currency.Pop:
                if (amount > currentPop) { return false; }
                break;
        }
        return true;
    }

    public void RemoveCurrency(currency type, int amount)
    {
        switch (type)
        {
            case currency.Wood:
                wood -= amount;
                break;
            case currency.Food:
                food -= amount;
                break;
            case currency.Gold:
                gold -= amount;
                break;
            case currency.Stone:
                stone -= amount;
                break;
            case currency.Pop:
                currentPop -= amount;
                break;
        }
    }
}
