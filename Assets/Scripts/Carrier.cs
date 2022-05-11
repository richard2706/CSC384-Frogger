using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Carrier : MonoBehaviour
{
    private bool canCarry = true;
    private Transform carrierTransform;

    private void Awake()
    {
        carrierTransform = transform;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (canCarry && collider.GetComponentInParent<Carryable>())
        {
            collider.transform.parent = carrierTransform;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<Carryable>())
        {
            collider.transform.parent = null;
        }
    }
}
