using UnityEngine;

/// <summary>
/// Behaviour which causes the player to lose a life on contact.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Dangerous : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerManager player = collider.GetComponentInParent<PlayerManager>();
        if (player) player.StartPlayerHit();
    }
}
