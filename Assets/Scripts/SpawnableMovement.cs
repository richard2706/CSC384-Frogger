using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SpawnableMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform spawnableTransform;
    private Rigidbody2D spawnableRigidbody;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void Awake()
    {
        spawnableTransform = transform;
        spawnableRigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        PlayerLives.OnLevelLost += DisableMovement;
    }

    private void OnDisable()
    {
        PlayerLives.OnLevelLost -= DisableMovement;
    }

    private void FixedUpdate()
    {
        // Move the spawnable forwards at constant rate
        Vector2 forwardVector = new Vector2(spawnableTransform.right.x, -spawnableTransform.right.y);
        spawnableRigidbody.MovePosition(spawnableRigidbody.position + speed * Time.fixedDeltaTime * forwardVector);
    }

    private void DisableMovement()
    {
        enabled = false;
    }
}
