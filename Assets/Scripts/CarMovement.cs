using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarMovement : MonoBehaviour
{
    [SerializeField] private float minSpeedMultiplier;
    [SerializeField] private float maxSpeedMultiplier;
    private float speedMultiplier;
    private Rigidbody2D carBody;

    private void Awake()
    {
        carBody = GetComponent<Rigidbody2D>();
        speedMultiplier = Random.Range(minSpeedMultiplier, maxSpeedMultiplier);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move the car forwards at constant rate
        Vector2 forwardVector = new Vector2(-transform.right.x, -transform.right.y); // Vector in direction of sprite
        carBody.MovePosition(carBody.position + forwardVector * speedMultiplier * Time.fixedDeltaTime);
    }
}
