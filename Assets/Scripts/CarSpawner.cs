using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval = 2f; // Length of time between car spawns.
    [SerializeField] private CarMovement car; // Car prefab from which to spawn new cars.
    [SerializeField] private float carSpeed;
    [SerializeField] private bool spawnDirectionLeft; // If true, spawned cars move left, otherwise they move right.

    private Transform spawnPoint; // Location to spawn cars from.
    private float roadWidth;
    private float timeToNextSpawn = 0f; // Time until another car is spawned.

    private void Awake()
    {
        spawnPoint = transform;
        if (spawnDirectionLeft) spawnPoint.Rotate(0f, 0f, 180f);
    }

    private void Start()
    {
        roadWidth = spawnPoint.parent.gameObject.GetComponent<SpriteRenderer>().bounds.extents.x * 2;
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
        // Spawn car
        CarMovement carMovement = Instantiate(car, spawnPoint);
        carMovement.SetSpeed(carSpeed);

        // Destroy car when off screen
        float timeUntilDestroy = (roadWidth + 1) / carSpeed;
        Destroy(carMovement.gameObject, timeUntilDestroy);
    }
}
