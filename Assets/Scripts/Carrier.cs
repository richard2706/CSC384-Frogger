using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Carrier : MonoBehaviour
{
    protected bool canCarry = true;
    public DangerousTerrain Terrain { get; set; } // terrain this carrier is over

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Carryable carryableTarget = collider.GetComponentInParent<Carryable>();
        if (canCarry && carryableTarget)
        {
            carryableTarget.transform.parent = transform;
            carryableTarget.BeingCarried = true;
        }
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        Carryable carryableTarget = collider.GetComponentInParent<Carryable>();
        if (carryableTarget)
        {
            carryableTarget.transform.parent = null;
            carryableTarget.BeingCarried = false;

            PlayerManager player = collider.GetComponentInParent<PlayerManager>();
            if (Terrain && player)
            {
                Terrain.CheckPlayerTerrainCollision(player);
            }
        }
    }
}
