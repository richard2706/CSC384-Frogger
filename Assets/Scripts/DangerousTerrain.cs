using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour which causes the player to lose a life if they are not being carried by a <see cref="Carrier"/>.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DangerousTerrain : Dangerous
{
    private List<PlayerManager> playersOnTerrain;

    /// <summary>
    /// Determines if player should be damaged by checking if the player is still being carried.
    /// </summary>
    public void CheckPlayerTerrainCollision(PlayerManager player)
    {
        StartCoroutine(ExecuteCheckPlayerTerrainCollision(player));
    }

    private void Awake()
    {
        playersOnTerrain = new List<PlayerManager>();
    }

    private IEnumerator ExecuteCheckPlayerTerrainCollision(PlayerManager player)
    {
        bool playerOnTerrain = playersOnTerrain.Contains(player);
        yield return new WaitForFixedUpdate();
        if (playerOnTerrain && !player.GetComponent<Carryable>().BeingCarried)
        {
            player.PlayerLoseLife();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerManager player = collider.GetComponentInParent<PlayerManager>();
        if (player)
        {
            playersOnTerrain.Add(player);
            CheckPlayerTerrainCollision(player);
            return;
        }

        Carrier carrier = collider.GetComponentInParent<Carrier>();
        if (carrier)
        {
            carrier.Terrain = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        PlayerManager player = collider.GetComponentInParent<PlayerManager>();
        if (playersOnTerrain.Contains(player))
        {
            playersOnTerrain.Remove(player);
        }
    }
}
