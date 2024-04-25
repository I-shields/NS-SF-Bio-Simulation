using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRunner : MonoBehaviour
{
    public GameObject master;
    private SpawnBot sbRef;
    private Timer timer;
    private GameObject tron;
    private SpawnFood food;
    private HaveSex sexer;
    private BotSpawner spawner;
    private bool running = false;
    private GameObject ui;
    private Stats statMaster;
    // Start is called before the first frame update
    void Start()
    {
        timer = master.GetComponent<Timer>();
        food = master.GetComponent<SpawnFood>();
        spawner = master.GetComponent<BotSpawner>();
        sexer = master.GetComponent<HaveSex>();
        ui = GameObject.FindGameObjectWithTag("Ui");
        statMaster = ui.GetComponent<Stats>();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            running = true;
            spawner.looping();
            timer.StartTimer();
        }

        if(Input.GetKeyDown(KeyCode.End))
        {
            running = false;
            statMaster.saveInfo();
        }

        if (running && !timer.timerRunning)
        {

            if(!timer.timerRunning)
            {
                sexer.nineMonthsLater();
                timer.StartTimer();
            }

        }
    }
}
