using System.Collections;
using UnityEngine;

/// <summary>
/// Behaviour which causes the player to lose a life if they are not being carried by a <see cref="Carrier"/>.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class DangerousTerrain : Dangerous
{
    private PlayerManager playerOnTerrain;

    /// <summary>
    /// Determines if player should be damaged by checking if the player is still being carried.
    /// </summary>
    public void CheckPlayerTerrainCollision()
    {
        StartCoroutine(ExecutePlayerTerrainCollisionCheck());
    }

    private IEnumerator ExecutePlayerTerrainCollisionCheck()
    {
        yield return new WaitForFixedUpdate();
        if (playerOnTerrain && !playerOnTerrain.GetComponent<Carryable>().BeingCarried)
        {
            playerOnTerrain.PlayerLoseLife();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerManager player = collider.GetComponentInParent<PlayerManager>();
        if (player)
        {
            playerOnTerrain = player;
            CheckPlayerTerrainCollision();
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
        if (collider.GetComponentInParent<PlayerManager>() == playerOnTerrain)
        {
            playerOnTerrain = null;
        }
    }
}
