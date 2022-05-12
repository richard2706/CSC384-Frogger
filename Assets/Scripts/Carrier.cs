using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Carrier : MonoBehaviour
{
    private bool canCarry = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Carryable carryableTarget = collider.GetComponentInParent<Carryable>();
        if (canCarry && carryableTarget) collider.transform.parent = transform;
    }

    protected virtual void OnTriggerExit2D(Collider2D collider)
    {
        Carryable carryableTarget = collider.GetComponentInParent<Carryable>();
        if (carryableTarget) collider.transform.parent = null;
    }
}
