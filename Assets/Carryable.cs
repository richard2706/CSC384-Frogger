using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Carryable : MonoBehaviour
{
    private bool beingCarried = false;

    private void OnEnable()
    {
        Carrier.OnCarryableEnter += MoveWithCarrier;
    }

    private void OnDisable()
    {
        Carrier.OnCarryableEnter -= MoveWithCarrier;
    }

    private void MoveWithCarrier(Carrier carrier)
    {
        gameObject.transform.parent = carrier.transform;
        beingCarried = true;
    }
}
