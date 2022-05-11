using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Carrier : MonoBehaviour
{
    public static event Action<Carrier> OnCarryableEnter;
    //public static event Action<Carrier> OnCarryableLeave;

    private bool canCarry = true;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (canCarry && collider.GetComponentInParent<Carryable>())
        {
            Debug.Log("Carryable touched a carrier");
            OnCarryableEnter?.Invoke(this);
        }
    }
}
