using UnityEngine;

public class HomeManager : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponentInParent<PlayerMovement>())
        {
            Debug.Log("Player reached a home");
        }
    }
}
