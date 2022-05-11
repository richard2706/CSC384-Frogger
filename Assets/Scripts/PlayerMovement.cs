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
            MovePlayer();
        }
        nextMovement = Vector2.zero;
    }

    private void MovePlayer() // must also check if home is already filled
    {
        Vector2 newPosition = playerBody.position + nextMovement;

        // Check new player position is valid
        bool atFrogHome = false; // only check for this if at top of game board
        if (newPosition.y > yMaxBound)
        {
            foreach (Vector2 homePosition in HomeManager.AllHomePositions)
            {
                atFrogHome = Vector2.Distance(homePosition, newPosition) < playerCollider.radius;
                Debug.Log(atFrogHome);
                if (atFrogHome) break;
            }
        }
        bool withinGameArea = newPosition.x <= xMaxBound && newPosition.x >= xMinBound
            && newPosition.y <= yMaxBound && newPosition.y >= yMinBound;

        // Execute movement
        if (withinGameArea || atFrogHome) playerBody.MovePosition(newPosition);
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

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<CarMovement>() || collider.GetComponentInParent<HomeManager>())
            resetPosition = true;
    }
}
