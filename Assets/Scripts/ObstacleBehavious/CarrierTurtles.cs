using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CarrierTurtles : Carrier
{
    [SerializeField] private float floatingTime = 2f;
    [SerializeField] private float sinkTransitionTime = 1f;
    [SerializeField] private float underwaterTime = 0.5f;
    [SerializeField] private float surfaceTransitionTime = 0.25f;
    private float firstSinkTime;

    [SerializeField] private Sprite floatingSprite;
    [SerializeField] private Sprite transitionSprite;
    [SerializeField] private Sprite underwaterSprite;
    private SpriteRenderer spriteRenderer;
    private Collider2D turtleCollider;

    private void Awake()
    {
        firstSinkTime = Random.Range(0, floatingTime);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = floatingSprite;
        turtleCollider = GetComponent<Collider2D>();
        SetCanCarry(true);
    }

    private void OnEnable()
    {
        PlayerLives.OnGameOver += StopAnimation;
    }

    private void OnDisable()
    {
        PlayerLives.OnGameOver -= StopAnimation;
    }

    private void Start()
    {
        StartCoroutine(ToggleTurtles());
    }

    private IEnumerator ToggleTurtles()
    {
        yield return new WaitForSeconds(firstSinkTime);
        while (true)
        {
            spriteRenderer.sprite = transitionSprite;
            yield return new WaitForSeconds(sinkTransitionTime);

            spriteRenderer.sprite = underwaterSprite;
            SetCanCarry(false);
            yield return new WaitForSeconds(underwaterTime);

            SetCanCarry(true);
            spriteRenderer.sprite = transitionSprite;
            yield return new WaitForSeconds(surfaceTransitionTime);

            spriteRenderer.sprite = floatingSprite;
            yield return new WaitForSeconds(floatingTime);
        }
    }

    private void SetCanCarry(bool canCarry)
    {
        this.canCarry = canCarry;
        turtleCollider.enabled = canCarry;
    }

    private void StopAnimation()
    {
        Debug.Log("stop turtle animation");
        StopAllCoroutines();
    }
}
