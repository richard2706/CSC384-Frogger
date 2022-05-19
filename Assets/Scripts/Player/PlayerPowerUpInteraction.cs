using UnityEngine;

public class PlayerPowerUpInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode usePowerUpKey;

    private PowerUp powerUp;
    private bool usePowerUp;
    private PlayerHeldPowerUp powerUpHolder;

    public void PickUpPowerUp(PowerUp powerUp)
    {
        powerUpHolder.DisplayPowerUp(powerUp);
        this.powerUp = powerUp;
    }

    private void Awake()
    {
        usePowerUp = false;
        powerUpHolder = GetComponentInChildren<PlayerHeldPowerUp>();
    }

    private void Update()
    {
        if (powerUp != null && Input.GetKeyDown(usePowerUpKey)) usePowerUp = true;
    }

    private void FixedUpdate()
    {
        if (usePowerUp)
        {
            if (powerUp != null) UsePowerUp();
            usePowerUp = false;
        }
    }

    private void UsePowerUp()
    {
        powerUp.Use();
        powerUpHolder.HidePowerUp();
        powerUp = null;
    }
}
