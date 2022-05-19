public class SuperJumpPowerUp : PowerUp
{
    public override void Use()
    {
        PlayerMovement playerMovement = holdingPlayer.GetComponent<PlayerMovement>();
        if (playerMovement) playerMovement.SuperJump();
    }
}
