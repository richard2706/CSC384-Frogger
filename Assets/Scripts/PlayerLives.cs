using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(Rigidbody2D))]
public class PlayerLives : MonoBehaviour // raname to PlayerCollisionManager
{
    private Rigidbody2D playerBody;
    private Vector2 initialPosition = new Vector2(0f, -4.5f);

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<SpawnableMovement>())
        {
            Debug.Log("Car collided with player");
            playerBody.MovePosition(initialPosition);
        }
    }
}
