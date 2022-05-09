using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f; // Length of time between car spawns.
    [SerializeField] private GameObject car; // Car prefab from which to spawn new cars.
    [SerializeField] private bool spawnDirectionLeft; // If true, spawned cars move left, otherwise they move right.
    private Transform spawnPoint; // Location to spawn cars from.
    private float timeToNextSpawn = 0f; // Time until another car is spawned.

    private void Awake()
    {
        // adjust spawnDirection based on serialzefield option or towards centre of road
        spawnPoint = transform;
        if (spawnDirectionLeft) spawnPoint.Rotate(0f, 0f, 180f);
    }

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
        Instantiate(car, spawnPoint);
    }
}
