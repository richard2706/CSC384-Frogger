using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D)), RequireComponent(typeof(CircleCollider2D)), RequireComponent(typeof(Carryable))]
public class PlayerMovement : MonoBehaviour
{
    public static event Action<int> OnIncreaseMaxForwardStep;

    [SerializeField] private SpriteRenderer boundingGameArea; // Player can move within bounds of this sprite.

    // Bounding coordinates for player movement
    private float xMinBound;
    private float xMaxBound;
    private float yMinBound;
    private float yMaxBound;

    private Rigidbody2D playerBody;
    private CircleCollider2D playerCollider;

    private Vector2 nextMovement;
    private bool resetPosition;
    private Vector2 initialPosition;
    private int maxForwardSteps;
    private int currentForwardSteps;

    public void ResetPosition()
    {
        resetPosition = true;
        currentForwardSteps = 0;
    }

    public void SuperJump()
    {
        nextMovement = new Vector2(0, 2);
    }

    private void Awake()
    {
        playerBody = GetComponent<Rigidbody2D>();
        playerCollider = GetComponent<CircleCollider2D>();
        nextMovement = Vector2.zero;
        resetPosition = false;
        initialPosition = transform.position;
        maxForwardSteps = 0;
        currentForwardSteps = 0;
    }

    private void OnEnable()
    {
        FrogHome.OnFrogReachedHome += ResetMaxForwardSteps;
        PlayerLives.OnLevelLost += DisableMovement;
        FrogHome.OnLevelWon += DisableMovement;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= ResetMaxForwardSteps;
        PlayerLives.OnLevelLost -= DisableMovement;
        FrogHome.OnLevelWon -= DisableMovement;
    }

    private void Start()
    {
        // Calculate bounds so player cannot move outside the game area.
        Vector2 gameAreaCenter = boundingGameArea.transform.position;
        Vector2 gameAreaExtents = boundingGameArea.bounds.extents;
        xMinBound = gameAreaCenter.x - gameAreaExtents.x;
        xMaxBound = gameAreaCenter.x + gameAreaExtents.x;
        yMinBound = gameAreaCenter.y - gameAreaExtents.y;
        yMaxBound = gameAreaCenter.y + gameAreaExtents.y - Vector2.up.y;
    }

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
            if (PlayerPositionValid(newPosition))
            {
                playerBody.MovePosition(newPosition);
                UpdateForwardStepCount(nextMovement);
            }
        }
        nextMovement = Vector2.zero;
    }

    private void Update()
    {
        // Check for next movement direction
        if (Input.GetKeyDown(KeyCode.D)) nextMovement = Vector2.right;
        else if (Input.GetKeyDown(KeyCode.A)) nextMovement = Vector2.left;
        else if (Input.GetKeyDown(KeyCode.W)) nextMovement = Vector2.up;
        else if (Input.GetKeyDown(KeyCode.S)) nextMovement = Vector2.down;
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
            foreach (FrogHome home in FrogHome.AllHomes)
            {
                bool atFrogHome = Vector2.Distance(home.Position, playerPosition) <= (home.ColliderSize.x / 2);
                atEmptyFrogHome = atFrogHome && !home.IsFilled;
                if (atEmptyFrogHome) break;
            }
        }

        bool withinGameArea =
            playerPosition.x + playerCollider.radius <= xMaxBound
            && playerPosition.x - playerCollider.radius >= xMinBound
            && playerPosition.y <= yMaxBound
            && playerPosition.y >= yMinBound;

        return withinGameArea || atEmptyFrogHome;
    }

    private void UpdateForwardStepCount(Vector2 playerMovement)
    {
        int forwardSteps = (int) Math.Floor(playerMovement.y);
        currentForwardSteps += forwardSteps;
        if (currentForwardSteps > maxForwardSteps)
        {
            maxForwardSteps = currentForwardSteps;
            OnIncreaseMaxForwardStep?.Invoke(forwardSteps);
        }
    }

    private void DisableMovement()
    {
        enabled = false;
    }

    private void ResetMaxForwardSteps()
    {
        maxForwardSteps = 0;
    }
}
