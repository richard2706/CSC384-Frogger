using UnityEngine;

[RequireComponent(typeof(Collider2D)), RequireComponent(typeof(SpriteRenderer))]
public abstract class PowerUp : MonoBehaviour
{
    public bool AutoUsed { get; protected set; }

    private SpriteRenderer spriteRenderer;
    protected PlayerPowerUpInteraction holdingPlayer;

    public virtual void OnPickUp()
    {
        Debug.Log("Power up picked up");
    }

    public virtual void Use()
    {
        Debug.Log("Power up used");
    }

    public Sprite GetSprite() => spriteRenderer.sprite;

    private void Awake()
    {
        AutoUsed = false;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        PlayerPowerUpInteraction player = collider.GetComponent<PlayerPowerUpInteraction>();
        if (player)
        {
            holdingPlayer = player;
            OnPickUp();
            player.PickUpPowerUp(this);
            gameObject.SetActive(false);
        }
    }
}
