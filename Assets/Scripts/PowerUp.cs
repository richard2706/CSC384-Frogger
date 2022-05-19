using UnityEngine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
public abstract class PowerUp : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    public abstract void Use();

    public Sprite GetSprite() => spriteRenderer.sprite;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerPowerUpInteraction player = collider.GetComponent<PlayerPowerUpInteraction>();
        if (player)
        {
            player.PickUpPowerUp(this);
            gameObject.SetActive(false);
        }
    }
}
