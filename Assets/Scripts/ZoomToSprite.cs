using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomToSprite : MonoBehaviour
{
    private const float uiRowHeight = 0.5f;

    [SerializeField] private SpriteRenderer sprite;

    private Transform cameraTransform;
    private float topUIHeight;

    private void Awake()
    {
        cameraTransform = transform;
        topUIHeight = GameManager.Multiplayer ? uiRowHeight * 2 : uiRowHeight;
    }

    private void Start()
    {
        PositionCameraWithTopMargin();
        ZoomCameraToSprite();
    }

    private void PositionCameraWithTopMargin()
    {
        Vector3 spritePosition = sprite.transform.position;
        cameraTransform.position = new Vector3(spritePosition.x, spritePosition.y + topUIHeight, cameraTransform.position.z);
    }

    private void ZoomCameraToSprite()
    {
        Vector3 spriteExtents = sprite.GetComponent<SpriteRenderer>().bounds.extents;
        GetComponent<Camera>().orthographicSize = spriteExtents.y + topUIHeight;
    }
}
