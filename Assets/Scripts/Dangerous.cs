using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dangerous : MonoBehaviour
{
    [SerializeField] private bool safeIfCarried; // if true, player is safe if they are being carried

    private void OnTriggerEnter2D(Collider2D collider)
    {
        CheckPlayerCollision(collider);
    }

    private void OnTriggerStay2D(Collider2D collider)
    {
        CheckPlayerCollision(collider);
    }

    private void CheckPlayerCollision(Collider2D collider)
    {
        PlayerMovement player = collider.GetComponentInParent<PlayerMovement>();
        if (player && !(safeIfCarried && player.GetComponent<Carryable>().CheckBeingCarried()))
        {
            player.ResetPlayerPosition();
        }
    }
}
