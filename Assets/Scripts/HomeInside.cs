using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HomeInside : MonoBehaviour
{
    [SerializeField] private Sprite frogSprite;
    [SerializeField] private Sprite flySprite;

    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowFrog()
    {
        spriteRenderer.sprite = frogSprite;
    }

    public void ShowFly()
    {
        spriteRenderer.sprite = flySprite;
    }

    public void HideFly()
    {
        spriteRenderer.sprite = null;
    }
}
