using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelProgression : MonoBehaviour
{
    //references to objects to change
    [SerializeField] GameObject KrakenSpawner;
    [SerializeField] GameObject CarSpawner1;
    [SerializeField] GameObject CarSpawner2;
    [SerializeField] GameObject CarSpawner3;
    [SerializeField] GameObject CarSpawnerReverse1;
    [SerializeField] GameObject CarSpawnerReverse2;
    [SerializeField] GameObject CarSpawnerReverse3;
    [SerializeField] GameObject FishSpawner;
    private float timeToKraken;
    private float timeToChangeCars;
    private float timeToChangeCarsSpawnees;
    private float timeToChangeFish;

    private float ammountToChangeSpawners = 0.5f;

    bool krakenActive;

    int timesChanged; //so we cap the ammount of spawners being changed

    void Start()
    {
        timesChanged = 0;

        //kraken part
        timeToKraken = 0;
        krakenActive = false;

        //cars part
        timeToChangeCars = 0;
        timeToChangeCarsSpawnees = 0;

        //fish part
        timeToChangeFish = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //KRAKEN PART
        if (!krakenActive) { //so once kraken active, it won't need to update this timer
        timeToKraken += Time.deltaTime;
        }

        if (timeToKraken >= 60) //Kraken awakes!
        {
            //Debug.Log("***************1 minute passed");
            KrakenSpawner.SetActive(true);
            krakenActive = true;
        }

        if (timesChanged <= 5) { //this is an int to cap the ammount of this thing happening.
            //CARS PART
            timeToChangeCars += Time.deltaTime; //for spwaners
            timeToChangeCarsSpawnees += Time.deltaTime; //for max of spawnees

            if (timeToChangeCars >= 45)//every 45 secs will lower down the min and max ammount to spawn a car
            {
                //Debug.Log("***************45 secs passed");
                timeToChangeCars = 0;
                CarSpawner1.GetComponent<spawner>().ChangeMinimum(ammountToChangeSpawners);
                CarSpawner2.GetComponent<spawner>().ChangeMinimum(ammountToChangeSpawners);
                CarSpawner3.GetComponent<spawner>().ChangeMinimum(ammountToChangeSpawners);
                CarSpawnerReverse1.GetComponent<spawner>().ChangeMinimum(ammountToChangeSpawners);
                CarSpawnerReverse2.GetComponent<spawner>().ChangeMinimum(ammountToChangeSpawners);
                CarSpawnerReverse3.GetComponent<spawner>().ChangeMinimum(ammountToChangeSpawners);

                CarSpawner1.GetComponent<spawner>().ChangeMaximum(ammountToChangeSpawners);
                CarSpawner2.GetComponent<spawner>().ChangeMaximum(ammountToChangeSpawners);
                CarSpawner3.GetComponent<spawner>().ChangeMaximum(ammountToChangeSpawners);
                CarSpawnerReverse1.GetComponent<spawner>().ChangeMaximum(ammountToChangeSpawners);
                CarSpawnerReverse2.GetComponent<spawner>().ChangeMaximum(ammountToChangeSpawners);
                CarSpawnerReverse3.GetComponent<spawner>().ChangeMaximum(ammountToChangeSpawners);

                timesChanged++; //timesChanged counter changed
            }

            if (timeToChangeCarsSpawnees >= 60)//1 more car to the max ammount every minute
            {
                timeToChangeCarsSpawnees = 0;

                CarSpawner1.GetComponent<spawner>().ChangeMaxCars(1);
                CarSpawner2.GetComponent<spawner>().ChangeMaxCars(1);
                CarSpawner3.GetComponent<spawner>().ChangeMaxCars(1);
                CarSpawnerReverse1.GetComponent<spawner>().ChangeMaxCars(1);
                CarSpawnerReverse2.GetComponent<spawner>().ChangeMaxCars(1);
                CarSpawnerReverse3.GetComponent<spawner>().ChangeMaxCars(1);

            }

            //FISH PART
            timeToChangeFish += Time.deltaTime; //you can put all of this in the car part, but I wanted to keep things separated, they are changed
                                                //at the same time by the same ammount

            if (timeToChangeFish >= 145) //every 45 secs will lower down the min and max ammount to spawn a fish
            {
                timeToChangeFish = 0;
                FishSpawner.GetComponent<FishSpawner>().ChangeMinimum(ammountToChangeSpawners);
                FishSpawner.GetComponent<FishSpawner>().ChangeMaximum(ammountToChangeSpawners);

            }
        }               
    }
}
