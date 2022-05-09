using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 0.3f; // Length of time between car spawns.
    [SerializeField] private GameObject car; // Car prefab from which to spawn new cars.
    private float timeToNextSpawn = 0f; // Time until another car is spawned.

    private void Update()
    {
        if (timeToNextSpawn <= 0)
        {
            SpawnCar();
            timeToNextSpawn += spawnInterval;
        }
        else
        {
            timeToNextSpawn -= Time.deltaTime;
        }
    }

    private void SpawnCar()
    {
        Instantiate(car);
    }
}
