using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite frogSprite;
    [SerializeField] private Sprite ripFrogSprite;
    [SerializeField] private float restartDelay;

    private SpriteRenderer spriteRenderer;

    public void StartLoseLifeSequence()
    {
        StartCoroutine(LoseLifeSequence());
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private IEnumerator LoseLifeSequence()
    {
        //enabled = false;
        spriteRenderer.sprite = ripFrogSprite;
        yield return new WaitForSeconds(restartDelay);

        spriteRenderer.sprite = frogSprite;
        //enabled = true;
    }
}
