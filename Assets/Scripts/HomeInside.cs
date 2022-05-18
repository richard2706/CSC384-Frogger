using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class HomeInside : MonoBehaviour
{
    [SerializeField] private Sprite frogSprite;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void ShowFrog()
    {
        spriteRenderer.sprite = frogSprite;
    }
}
