using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dangerous : MonoBehaviour
{
    [SerializeField] private bool safeIfCarried; // if true, player is safe if they are being carried

    public void CheckPlayerContact(PlayerMovement player)
    {
        bool isPlayerInContact = GetComponent<Collider2D>().IsTouching(player.GetComponent<Collider2D>());
        Debug.Log(GetComponent<Collider2D>().);
        if (isPlayerInContact) CheckPlayerSafe(player);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerMovement player = collider.GetComponentInParent<PlayerMovement>();
        if (player) CheckPlayerSafe(player);
    }

    private void CheckPlayerSafe(PlayerMovement player)
    {
        if (!(safeIfCarried && player.GetComponent<Carryable>().CheckBeingCarried()))
        {
            player.ResetPlayerPosition();
        }
    }
}
