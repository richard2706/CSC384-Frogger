using System;
using UnityEngine;

// Ensures there is a Rigidbody2D component on any game object that this script is added to.
[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D))]
public class PlayerMovement : MonoBehaviour
{
    // Bounding coordinates for player movement
    private static float xMinBound;
    private static float xMaxBound;
    private static float yMinBound;
    private static float yMaxBound;

    private Rigidbody2D playerBody;
    private CircleCollider2D playerCollider;
    private Vector2 nextMovement;
    private bool resetPosition;
    private Vector2 initialPosition;

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        nextMovement = Vector2.zero;
        resetPosition = false;
        initialPosition = transform.position;
    }

    private void Start()
    {
        // Calculate road bounds so player cannot move outside the road area.
        Vector3 roadExtents = transform.parent.gameObject.GetComponent<SpriteRenderer>().bounds.extents;
        xMinBound = -roadExtents.x;
        xMaxBound = roadExtents.x;
        yMinBound = -roadExtents.y;
        yMaxBound = roadExtents.y - Vector2.up.y;
    }

    // Called at regular fixed intervals
    private void FixedUpdate()
    {
        if (resetPosition)
        {
            playerBody.MovePosition(initialPosition);
            resetPosition = false;
        }
        else if (nextMovement != Vector2.zero)
        {
            Vector2 newPosition = playerBody.position + nextMovement;
            if (PlayerPositionValid(newPosition)) playerBody.MovePosition(newPosition);
        }
        nextMovement = Vector2.zero;
    }

    /// <summary>
    /// Returns true if the given position is a valid position for the player to move to.
    /// </summary>
    /// <returns>True if the position is a valid position for the player to move to.</returns>
    private bool PlayerPositionValid(Vector2 position)
    {
        bool atFrogHome = false;
        if (position.y > yMaxBound)
        {
            foreach (Vector2 homePosition in HomeManager.AllHomePositions)
            {
                atFrogHome = Vector2.Distance(homePosition, position) < playerCollider.radius;
                if (atFrogHome) break;
            }
        }

        bool withinGameArea = position.x <= xMaxBound && position.x >= xMinBound
            && position.y <= yMaxBound && position.y >= yMinBound;

        return withinGameArea || atFrogHome;
    }

    void Update()
    {
        // Check for next movement direction
        if (Input.GetKeyDown(KeyCode.D)) nextMovement = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.A)) nextMovement = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.W)) nextMovement = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S)) nextMovement = Vector2.down;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<CarMovement>() || collider.GetComponentInParent<HomeManager>())
            resetPosition = true;
    }
}
