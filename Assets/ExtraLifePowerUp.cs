using UnityEngine;

public class ExtraLifePowerUp : PowerUp
{
    //private PlayerLives playerLives;

    public override void OnPickUp()
    {
        base.OnPickUp();
        holdingPlayer.GetComponent<PlayerLives>().ApplyExtraLife();
    }

    public override void Use()
    {
        base.Use();
        Debug.Log("Use Extra Life");
    }

    private void Start()
    {
        AutoUsed = true;
    }
}
