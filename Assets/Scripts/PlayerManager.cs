using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerSpriteManager)), RequireComponent(typeof(PlayerLives))]
public class PlayerManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private PlayerSpriteManager spriteManager;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteManager = GetComponent<PlayerSpriteManager>();
    }

    private void OnEnable()
    {
        FrogHome.OnFrogReachedHome += PlayerHit;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= PlayerHit;
    }

    public void PlayerHit()
    {
        playerMovement.ResetPosition();
        spriteManager.StartLoseLifeSequence();
    }
}
