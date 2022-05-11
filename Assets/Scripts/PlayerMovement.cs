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

    private Transform playerTransform;
    private Rigidbody2D playerBody;
    private CircleCollider2D playerCollider;

    private Vector2 nextMovement;
    private bool resetPosition;
    private Vector2 initialPosition;

    private void Awake()
    {
        playerTransform = transform;
        playerBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        nextMovement = Vector2.zero;
        resetPosition = false;
        initialPosition = playerTransform.position;
    }

    private void Start()
    {
        // Calculate road bounds so player cannot move outside the road area.
        Vector3 roadExtents = playerTransform.parent.gameObject.GetComponent<SpriteRenderer>().bounds.extents;
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
    private bool PlayerPositionValid(Vector2 playerPosition)
    {
        bool atEmptyFrogHome = false;
        if (playerPosition.y > yMaxBound)
        {
            foreach (HomeManager home in HomeManager.AllHomes)
            {
                bool atFrogHome = Vector2.Distance(home.Position, playerPosition) < playerCollider.radius;
                atEmptyFrogHome = atFrogHome && !home.IsTaken;
                if (atEmptyFrogHome) break;
            }
        }

        bool withinGameArea = playerPosition.x <= xMaxBound && playerPosition.x >= xMinBound
            && playerPosition.y <= yMaxBound && playerPosition.y >= yMinBound;

        return withinGameArea || atEmptyFrogHome;
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
