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

    /* Rigidbody component of the player. */
    private Rigidbody2D rb;

    /* Vector to move by in the next call to FixedUpdate, as decied by the most recent directional
     * input key from the player. */
    private Vector2 nextMovement;

    /*
     * Called once before the game object is created. No guarantee that all other game objects exist here.
     * Should only contain code related to this game object.
     */
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        nextMovement = Vector2.zero;
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

    // Called at regular fixed intervals
    private void FixedUpdate()
    {
        // Execute movement (if detected in Update)
        Vector2 newPosition = rb.position + nextMovement;
        bool withinBounds = newPosition.x <= xMaxBound && newPosition.x >= xMinBound
            && newPosition.y <= yMaxBound && newPosition.y >= yMinBound;
        if (withinBounds) rb.MovePosition(newPosition);

        // Reset next movement vector to zero
        nextMovement = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        // Check for next movement direction
        if (Input.GetKeyDown(KeyCode.D)) nextMovement = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.A)) nextMovement = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.W)) nextMovement = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S)) nextMovement = Vector2.down;
    }
}
