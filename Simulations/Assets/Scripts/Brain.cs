using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpawnBot : MonoBehaviour
{
    public LayerMask foodLayer;

    public float range = 2f;
    public float moveSpeed = 2f;
    public float energy = 100;
    public float personalTimer = 0f;
    public float interval = 0.5f;
    public bool displayed = false;
    public int foodCount;
    private GameObject nearestFood;
    public Vector3 home;
    private GameObject master;
    private Timer timer;
    public float travelTime = 0;
    public float initalEnergy;
    public float travelEnergy;
    public bool isHome = false;
    public bool getFood = true;
    public bool hasHouse = false;
    private Rigidbody2D rb;
    private Vector2 circleCenter;
    private float circleRadius;
    private Vector2 lastPos;

    void Start()
    {
        home = gameObject.transform.position;
        initalEnergy = energy;
        master = GameObject.FindGameObjectWithTag("Master");
        timer = master.GetComponent<Timer>();
    }

    // Update is called once per frame
    void Update()
    {

        lastPos = gameObject.transform.position;
        if (!hasHouse)
        {
            GameObject homePrefab = Resources.Load<GameObject>("Home");
            Instantiate(homePrefab, home, Quaternion.identity);
            hasHouse = true;
        }

        if(timer.timerRunning && energy > 0)
        {
            dailyActivitys();
            personalTimer += Time.deltaTime;

            if(personalTimer >= interval)
            {
                personalTimer = 0f;
                energyHandler();
                if (nearestFood == null && energy > 0)
                {
                    moveRandom();
                }
            }
        }
        else if(energy == 0)
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
        }
    }

    void dailyActivitys()
    {
        rb = GetComponent<Rigidbody2D>();
        float distanceToCenter = Vector2.Distance(gameObject.transform.position, circleCenter);
        circleCenter = home;
        circleRadius = 0.2f;
        if (timer.timerRunning)
        {
            personalTimer += Time.deltaTime;
            travelEnergy = initalEnergy - energy;
            Vector2 homeDirection = (home - transform.position).normalized;

            if (timer.currentTime <= 3.5 && energy > 0 || travelEnergy + 3 >= energy && energy > 0)
            {
                Vector2 moveVelocity = homeDirection * moveSpeed;
                rb.velocity = moveVelocity;
                getFood = false;


                if (distanceToCenter < circleRadius && !isHome)
                {
                    isHome = true;
                    rb.MovePosition(home);
                }
            }

            if (nearestFood == null || !nearestFood.activeInHierarchy && energy > 0 && getFood)
            {
                findFood();
            }

            // Move towards the nearest food if it exists
            if (nearestFood != null && energy > 0 && getFood)
            {
                Vector2 direction = (nearestFood.transform.position - transform.position).normalized;
                Vector2 moveVelocity = direction * moveSpeed;
                rb.velocity = moveVelocity;
            }

        }
    }

    void findFood()
    {
        nearestFood = null;

        // Perform a circle cast to detect food within sight range
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, Vector2.up, Mathf.Infinity, foodLayer);

        float nearestDistance = Mathf.Infinity;

        // Check each hit
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.collider.CompareTag("Food"))
            {
                float distance = Vector2.Distance(transform.position, hit.collider.transform.position);
                if (distance < nearestDistance)
                {
                    // Set the nearest food if it's closer than the previous nearest
                    nearestFood = hit.collider.gameObject;
                    nearestDistance = distance;
                }
            }
        }
    }

    void energyHandler()
    {
        energy -= (int)moveSpeed;
        energy = Mathf.Max(energy, 0f);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Food"))
        {
            Destroy(other.gameObject);
            foodCount++;
            findFood();
        }
    }

    void moveRandom()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        float minX = -10.4f;
        float maxX = 10.4f;
        float minY = -4.6f;
        float maxY = 4.6f;

        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector2 randPos = new Vector2(randomX, randomY);
        Vector2 randDirection = new Vector2(randPos.x - gameObject.transform.position.x, randPos.y - gameObject.transform.position.y).normalized;
        Vector2 movetorand = randDirection * moveSpeed;
        rb.velocity = movetorand;


    }
}

