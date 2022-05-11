using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Dangerous : MonoBehaviour
{
    public static event Action OnDangerousCollision;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<PlayerMovement>()) OnDangerousCollision?.Invoke();
    }
}
