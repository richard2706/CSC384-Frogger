using UnityEngine;

/// <summary>
/// Behaviour which causes the player to lose a life if they are not being carried by a <see cref="Carrier"/>.
/// </summary>
public class DangerousTerrain : Dangerous
{
    protected override void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerManager player = collider.GetComponentInParent<PlayerManager>();
        if (player && !player.GetComponent<Carryable>().CheckBeingCarried())
        {
            player.StartPlayerHit();
        }
    }
}
