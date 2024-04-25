using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class HaveSex : MonoBehaviour
{
    private GameObject[] bots;
    private GameObject[] houses;
    private SpawnBot sbRef;
    private Timer timer;

    public int iteration = 0;

    private float minX = -10.55f;
    private float maxX = 10.55f;
    private float minY = -4.8f;
    private float maxY = 4.8f;

    public struct parents
    {
        public float speed;
        public int energy;
        public float range;
    }

    private List<parents> parentSets;
    private GameObject tron;
    private SpawnFood food;

    void Start()
    {
        timer = gameObject.GetComponent<Timer>();
        food = gameObject.GetComponent<SpawnFood>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            bots = GameObject.FindGameObjectsWithTag("Tron");
            nineMonthsLater();
        }
    }

    public void nineMonthsLater()
    {
        iteration++;
        bots = GameObject.FindGameObjectsWithTag("Tron");
        tron = Resources.Load<GameObject>("TronTron");
        foreach (GameObject bot in bots)
        {
            sbRef = bot.GetComponent<SpawnBot>();
            if(sbRef.isHome)
            {
                if(sbRef.foodCount > 2)
                {
                    if (Random.value < 0.5f)
                    {
                        heIsNotTheFather(bot);
                    }
                    else
                    {
                        float parentSpeed = bot.GetComponent<SpawnBot>().moveSpeed;
                        float parentRange = bot.GetComponent<SpawnBot>().range;
                        Debug.Log("Baby making time");
                        float randomX = UnityEngine.Random.Range(minX, maxX);
                        float randomY = UnityEngine.Random.Range(minY, maxY);
                        Vector2 randPos = new Vector2(randomX, randomY);
                        GameObject mutant = Instantiate(tron, randPos, Quaternion.identity);
                        sbRef = mutant.GetComponent<SpawnBot>();
                        sbRef.moveSpeed = parentSpeed;
                        sbRef.range = parentRange;
                    }
                }
                else if(sbRef.foodCount == 1)
                {
                    Debug.Log("Alive");
                }
                else
                {
                    Debug.Log("no food");
                    Destroy(bot);
                }
            }
            else
            {
                Debug.Log("not home");
                Destroy(bot);
            }
        }

        houses = GameObject.FindGameObjectsWithTag("Home");
        foreach (GameObject house in houses)
        {
            Destroy(house);
        }

        bots = GameObject.FindGameObjectsWithTag("Tron");
        foreach (GameObject bot in bots)
        {
            sbRef = bot.GetComponent<SpawnBot>();
            sbRef.foodCount = 0;
            sbRef.energy = 100;
            sbRef.initalEnergy = 100;
            sbRef.travelTime = 0;
            sbRef.getFood = true;
            sbRef.hasHouse = false;
            sbRef.personalTimer = 0;
            sbRef.isHome = false;

        }
        food.fillFood();

    }

    private void heIsNotTheFather(GameObject bot)
    {
        Debug.Log("Mutant!");
        float parentSpeed = bot.GetComponent<SpawnBot>().moveSpeed;
        float parentRange = bot.GetComponent<SpawnBot>().range;
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector2 randPos = new Vector2(randomX, randomY);
        GameObject mutant = Instantiate(tron, randPos, Quaternion.identity);
        sbRef = mutant.GetComponent<SpawnBot>();
        if (Random.value < 0.8f)
        {
            Debug.Log("Upgrades baby!");
            parentSpeed += Random.value;
            parentRange += Random.value;
        }
        else
        {
            Debug.Log("womp womp");
            parentSpeed -= Random.value;
            parentRange -= Random.value;
        }
        sbRef.moveSpeed = parentSpeed;
        sbRef.range = parentRange;
    }
}
