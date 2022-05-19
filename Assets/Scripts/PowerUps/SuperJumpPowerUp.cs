public class SuperJumpPowerUp : PowerUp
{
    public override void Use()
    {
        base.Use();
        PlayerMovement playerMovement = holdingPlayer.GetComponent<PlayerMovement>();
        if (playerMovement) playerMovement.SuperJump();
    }
}
