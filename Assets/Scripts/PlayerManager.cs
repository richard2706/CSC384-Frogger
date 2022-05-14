using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement)), RequireComponent(typeof(PlayerSpriteManager)), RequireComponent(typeof(PlayerLives))]
public class PlayerManager : MonoBehaviour
{
    public static event Action<PlayerLives> OnPlayerLoseLife;

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
        FrogHome.OnFrogReachedHome += playerMovement.ResetPosition;
    }

    private void OnDisable()
    {
        FrogHome.OnFrogReachedHome -= playerMovement.ResetPosition;
    }

    public void PlayerLoseLife()
    {
        StartCoroutine(ExecutePlayerLoseLife());
        playerLives.LoseLife();
        OnPlayerLoseLife?.Invoke(playerLives);
    }

    private IEnumerator ExecutePlayerLoseLife()
    {
        playerMovement.enabled = false;
        spriteManager.ShowRipSprite();
        yield return new WaitForSeconds(loseLifeRestartDelay);

        playerMovement.enabled = true;
        playerMovement.ResetPosition();
        yield return new WaitForFixedUpdate();
        spriteManager.ShowFrogSprite();
    }
}
