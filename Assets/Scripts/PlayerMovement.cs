using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures there is a Rigidbody2D component on any game object that this script is added to.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    // Called once before the game object is created
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
            rb.MovePosition(rb.position + Vector2.right);
        else if (Input.GetKeyDown(KeyCode.A))
            rb.MovePosition(rb.position + Vector2.left);
        else if (Input.GetKeyDown(KeyCode.W))
            rb.MovePosition(rb.position + Vector2.up);
        else if (Input.GetKeyDown(KeyCode.S))
            rb.MovePosition(rb.position + Vector2.down);
    }
}
