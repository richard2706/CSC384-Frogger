using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float spawnInterval; // Time in seconds between spawns.
    [SerializeField] private float spawnableSpeed;
    [SerializeField] private List<SpawnableMovement> spawnables;
    [SerializeField] private bool spawnDirectionLeft; // If true, spawnabled will travel left, otherwise they move right.

    private Transform spawnPoint; // Location to spawn from.
    private float timeToNextSpawn = 0f; // Time until the next spawnable will be spawned.

    private void Awake()
    {
        spawnPoint = transform;
        if (spawnDirectionLeft) spawnPoint.Rotate(0f, 0f, 180f);
    }

    private void OnEnable()
    {
        PlayerLives.OnLevelLost += StopSpawning;
        FrogHome.OnLevelWon += StopSpawning;
    }

    private void OnDisable()
    {
        PlayerLives.OnLevelLost -= StopSpawning;
        FrogHome.OnLevelWon -= StopSpawning;
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
        int randomIndex = UnityEngine.Random.Range(0, spawnables.Count);
        SpawnableMovement spawnableMovement = Instantiate(spawnables[randomIndex], spawnPoint);
        spawnableMovement.SetSpeed(spawnableSpeed);

        // Destroy spawnable when off screen
        float timeUntilDestroy = Math.Abs(spawnPoint.position.x * 2 / spawnableSpeed);
        StartCoroutine(DestroySpawnable(spawnableMovement, timeUntilDestroy));
    }

    private IEnumerator DestroySpawnable(SpawnableMovement spawnable, float timeUntilDestroy)
    {
        yield return new WaitForSeconds(timeUntilDestroy);
        Destroy(spawnable.gameObject);
    }

    private void StopSpawning()
    {
        StopAllCoroutines();
        enabled = false;
    }
}
