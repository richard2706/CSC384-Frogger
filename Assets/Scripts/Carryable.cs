using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Carryable : MonoBehaviour
{
    public bool BeingCarried { get; set; }
}
