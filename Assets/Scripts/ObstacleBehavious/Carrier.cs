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
        if (canCarry && carryableTarget) collider.transform.parent = transform;
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        Carryable carryableTarget = collider.GetComponentInParent<Carryable>();
        if (carryableTarget)
        {
            collider.transform.parent = null;

            if (Terrain && collider.GetComponentInParent<PlayerManager>())
            {
                StartCoroutine(CheckPlayerTerrainCollision());
            }
        }
    }

    private IEnumerator CheckPlayerTerrainCollision()
    {
        yield return new WaitForFixedUpdate();
        Terrain.CheckPlayerCollision();
    }
}
