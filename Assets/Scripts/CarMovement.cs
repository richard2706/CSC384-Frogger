using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CarMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    private Transform carTransform;
    private Rigidbody2D carBody;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    private void Awake()
    {
        carTransform = transform;
        carBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Move the car forwards at constant rate
        Vector2 forwardVector = new Vector2(carTransform.right.x, -carTransform.right.y); // Vector in direction of sprite
        carBody.MovePosition(carBody.position + speed * Time.fixedDeltaTime * forwardVector);
    }
}
