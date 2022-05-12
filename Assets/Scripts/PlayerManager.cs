using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerSpriteManager)), RequireComponent(typeof(PlayerLives))]
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float loseLifeRestartDelay;

    private PlayerMovement playerMovement;
    private PlayerSpriteManager spriteManager;
    private PlayerLives playerLives;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        spriteManager = GetComponent<PlayerSpriteManager>();
        playerLives = GetComponent<PlayerLives>();
    }

    private void OnEnable()
    {
        FrogHome.OnFrogReachedHome += StartPlayerHit;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= StartPlayerHit;
    }

    public void StartPlayerHit()
    {
        StartCoroutine(PlayerHit());
    }

    private IEnumerator PlayerHit()
    {
        spriteManager.ShowRipSprite();
        playerLives.LoseLife();
        yield return new WaitForSeconds(loseLifeRestartDelay);
        playerMovement.ResetPosition();
        yield return new WaitForFixedUpdate();
        spriteManager.ShowFrogSprite();
    }
}
