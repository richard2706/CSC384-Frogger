using UnityEngine;

[RequireComponent(typeof(PlayerLives))]
public class PlayerPowerUpInteraction : MonoBehaviour
{
    [SerializeField] private KeyCode usePowerUpKey;

    private PowerUp powerUp;
    private bool usePowerUp;
    private PlayerHeldPowerUp powerUpHolder;
    private PlayerLives playerLives;

    public void PickUpPowerUp(PowerUp powerUp)
    {
        powerUpHolder.DisplayPowerUp(powerUp);
        this.powerUp = powerUp;
    }

    public void UsePowerUp()
    {
        powerUp.Use();
        LosePowerUp();
    }

    private void Awake()
    {
        usePowerUp = false;
        powerUpHolder = GetComponentInChildren<PlayerHeldPowerUp>();
        playerLives = GetComponent<PlayerLives>();
    }

    private void OnEnable()
    {
        PlayerLives.OnPlayerLoseLife += LosePowerUp;
    }

    private void OnDisable()
    {
        PlayerLives.OnPlayerLoseLife -= LosePowerUp;
    }

    private void Update()
    {
        if (powerUp != null && !powerUp.AutoUsed && Input.GetKeyDown(usePowerUpKey)) usePowerUp = true;
    }

    private void FixedUpdate()
    {
        if (usePowerUp)
        {
            if (powerUp != null) UsePowerUp();
            usePowerUp = false;
        }
    }

    private void LosePowerUp()
    {
        powerUpHolder.HidePowerUp();
        powerUp = null;
    }

    private void LosePowerUp(PlayerLives playerLives)
    {
        if (this.playerLives == playerLives) LosePowerUp();
    }
}
