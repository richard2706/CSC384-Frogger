using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RiverCarrier : Carrier
{
    [SerializeField] private Dangerous river; // River over which this carrier carrys objects.

    protected override void OnTriggerExit2D(Collider2D collider)
    {
        base.OnTriggerExit2D(collider);

        PlayerMovement player = collider.GetComponentInParent<PlayerMovement>();
        if (player) river.CheckPlayerContact(player);
    }
}
