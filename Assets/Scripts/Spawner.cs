using System;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval; // Time in seconds between spawns.
    [SerializeField] private float spawnableSpeed;
    [SerializeField] private SpawnableMovement spawnable;
    [SerializeField] private bool spawnDirectionLeft; // If true, spawnabled will travel left, otherwise they move right.

    private Transform spawnPoint; // Location to spawn from.
    private float timeToNextSpawn = 0f; // Time until the next spawnable will be spawned.

    private void Awake()
    {
        spawnPoint = transform;
        if (spawnDirectionLeft) spawnPoint.Rotate(0f, 0f, 180f);
    }

    private void Update()
    {
        if (timeToNextSpawn <= 0)
        {
            Spawn();
            timeToNextSpawn += spawnInterval;
        }
        else
        {
            timeToNextSpawn -= Time.deltaTime;
        }
    }

    private void Spawn()
    {
        SpawnableMovement spawnableMovement = Instantiate(spawnable, spawnPoint);
        spawnableMovement.SetSpeed(spawnableSpeed);

        // Destroy spawnable when off screen
        float timeUntilDestroy = Math.Abs(spawnPoint.position.x * 2 / spawnableSpeed);
        Destroy(spawnableMovement.gameObject, timeUntilDestroy);
    }
}
