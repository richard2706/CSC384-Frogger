using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Carryable : MonoBehaviour
{
    public bool CheckBeingCarried()
    {
        List<Collider2D> colliders = new List<Collider2D>();
        ContactFilter2D filter = new ContactFilter2D().NoFilter();
        GetComponent<Collider2D>().OverlapCollider(filter, colliders);
        return colliders.Exists(IsBeingCarried);
    }

    /// <summary>
    /// Returns true if the given collider's gameObject is carrying this Carryable.
    /// </summary>
    /// <param name="collider"></param>
    /// <returns>True if the given collider's gameObject is carrying this Carryable.</returns>
    private bool IsBeingCarried(Collider2D collider)
    {
        return collider.GetComponentInParent<Carrier>();
    }
}
