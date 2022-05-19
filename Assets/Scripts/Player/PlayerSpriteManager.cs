using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerSpriteManager : MonoBehaviour
{
    [SerializeField] private Sprite ripFrogSprite;

    private SpriteRenderer spriteRenderer;
    private Sprite frogSprite;

    public void ShowRipSprite() // 2 public methods to change sprites? or coroutine to manage the changes?
    {
        spriteRenderer.sprite = ripFrogSprite;
    }

    public void ShowFrogSprite()
    {
        spriteRenderer.sprite = frogSprite;
    }

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        frogSprite = spriteRenderer.sprite;
    }
}
