using System;
using UnityEngine;

public class Carrier : MonoBehaviour
{
    private bool canCarry = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<Carryable>())
        {
            Debug.Log("Carryable touched a carrier");
        }
    }
}
