using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stats : MonoBehaviour
{
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI rangeText;
    public TextMeshProUGUI iterText;
    public TextMeshProUGUI botCount;

    private float AverageSpeed;
    private float AverageRange;
    private int iter;
    private int bcn;
    private GameObject master;
    private GameObject[] bots;
    private List<int> allBots;
    private List<int> iterCount;


    void Start()
    {
        allBots = new List<int>();
        iterCount = new List<int>();
    }

    void Update()
    {

    }

    public void giveStats()
    {
        bots = GameObject.FindGameObjectsWithTag("Tron");
        master = GameObject.FindGameObjectWithTag("Master");

        foreach(GameObject bot in bots)
        {
            AverageSpeed += bot.GetComponent<SpawnBot>().moveSpeed;
            bcn++;
        }
        AverageSpeed = Mathf.Round(AverageSpeed / bots.Length * 100.0f) / 100.0f;
        speedText.text = ("Average Speed: " + AverageSpeed.ToString());
        botCount.text = ("Bots: " + bcn.ToString());
        allBots.Add(bcn);
        bcn = 0;

        foreach (GameObject bot in bots)
        {
            AverageRange += bot.GetComponent<SpawnBot>().range;
        }
        AverageRange = Mathf.Round(AverageRange / bots.Length * 100.0f) / 100.0f;
        rangeText.text = ("Average Range: " + AverageRange.ToString());

        iter = master.GetComponent<HaveSex>().iteration;
        iterText.text = ("Iteration: " + iter.ToString());
        iterCount.Add(iter);

    }

    public void saveInfo()
    {
       string filePath = "Assets/Stats/botCount.txt";

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            Debug.Log("Saveing...");
            // Write each item in the list to the text file
            for(int i = 0; i <allBots.Count; i++)
            {
                writer.WriteLine("Bots: " + allBots[i].ToString() + " Iteration: " + iterCount[i].ToString());
            }
        }
    }
}
