using UnityEngine;

public class InteractWithPowerUp : MonoBehaviour
{
    [SerializeField] private KeyCode usePowerUpKey;

    private PowerUp powerUp;
    private bool usePowerUp;

    public void PickUpPowerUp(PowerUp powerUp)
    {
        this.powerUp = powerUp;
    }

    private void Awake()
    {
        usePowerUp = false;
    }

    private void Update()
    {
        if (powerUp != null && Input.GetKeyDown(usePowerUpKey)) usePowerUp = true;
    }

    private void FixedUpdate()
    {
        if (usePowerUp)
        {
            usePowerUp = false;
            if (powerUp != null)
            {
                powerUp.Use();
                powerUp = null;
            }
        }
    }
}
