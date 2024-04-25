using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotSpawner : MonoBehaviour
{
    public GameObject TronPrefab;
    public int NumOfBots;
    private BoxCollider2D collider;
    private float minX = -10f;
    private float maxX = 10f;
    private float minY = -4f;
    private float maxY = 4f;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            looping();
        }
    }

    void SpawnBots()
    {
        float randomX = UnityEngine.Random.Range(minX, maxX);
        float randomY = UnityEngine.Random.Range(minY, maxY);
        Vector2 randPos = new Vector2(randomX, randomY);
        Instantiate(TronPrefab, randPos, Quaternion.identity);
    }

    public void looping()
    {
        for (int i = 0; i < NumOfBots; i++)
        {
            SpawnBots();
        }
    }

}
