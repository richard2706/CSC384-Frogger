using UnityEngine;

public class ExtraLifePowerUp : PowerUp
{
    public override void OnPickUp()
    {
        base.OnPickUp();
        holdingPlayer.GetComponent<PlayerLives>().ApplyExtraLife();
    }

    private void Start()
    {
        AutoUsed = true;
    }
}
