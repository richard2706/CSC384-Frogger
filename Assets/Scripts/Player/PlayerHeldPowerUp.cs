using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHeldPowerUp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public void DisplayPowerUp(PowerUp powerUp)
    {
        spriteRenderer.sprite = powerUp.GetSprite();
    }

    public void HidePowerUp()
    {
        spriteRenderer.sprite = null;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
