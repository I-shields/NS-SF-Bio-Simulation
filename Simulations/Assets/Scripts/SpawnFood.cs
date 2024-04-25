using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpawnFood : MonoBehaviour
{
    public int amountToSpawn;
    private int amountSpawned;

    public GameObject ItemToSpawn;

    private float minX = -10.4f;
    private float maxX = 10.4f;
    private float minY = -4.6f;
    private float maxY = 4.6f;



    void Start()
    {
        fillFood();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void fillFood()
    {
        GameObject[] food = GameObject.FindGameObjectsWithTag("Food");
        amountSpawned = food.Length;

        if (amountSpawned < amountToSpawn)
        {
            for (int i = 0; i < amountToSpawn; i++)
            {
                float randomX = UnityEngine.Random.Range(minX, maxX);
                float randomY = UnityEngine.Random.Range(minY, maxY);
                Vector2 randPos = new Vector2(randomX, randomY);
                Instantiate(ItemToSpawn, randPos, Quaternion.identity);
            }
        }
    }
}
