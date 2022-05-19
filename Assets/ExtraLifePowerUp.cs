using UnityEngine;

public class ExtraLifePowerUp : PowerUp
{
    public override void OnPickUp()
    {
        base.OnPickUp();
        Debug.Log("Extra life picked up");
    }

    private void Start()
    {
        AutoUsed = true;
    }
}
