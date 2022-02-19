using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Ensures there is a Rigidbody2D component on any game object that this script is added to.
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    // Bounding coordinates for player movement
    private static float xMinBound;
    private static float xMaxBound;
    private static float yMinBound;
    private static float yMaxBound;

    private Rigidbody2D rb;

    /*
     * Called once before the game object is created. No guarantee that all other game objects exist here.
     * Should only contain code related to this game object.
     */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Called once before this game object is updated. All game objects will exist at this point.
    private void Start()
    {
        // Calculate road bounds so player cannot move outside the road area.
        Vector3 roadExtents = transform.parent.gameObject.GetComponent<SpriteRenderer>().bounds.extents;
        xMinBound = -roadExtents.x;
        xMaxBound = roadExtents.x;
        yMinBound = -roadExtents.y - Vector2.up.y; // Player can move along the bottom of the road
        yMaxBound = roadExtents.y + Vector2.up.y; // Player can cross to the other side of the road
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            Vector2 newPosition = rb.position + Vector2.right;
            if (newPosition.x <= xMaxBound) rb.MovePosition(newPosition);
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Vector2 newPosition = rb.position + Vector2.left;
            if (newPosition.x >= xMinBound) rb.MovePosition(newPosition);
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            Vector2 newPosition = rb.position + Vector2.up;
            if (newPosition.y <= yMaxBound) rb.MovePosition(newPosition);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Vector2 newPosition = rb.position + Vector2.down;
            if (newPosition.y >= yMinBound) rb.MovePosition(newPosition);
        }
    }
}
