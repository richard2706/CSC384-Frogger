using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomToSprite : MonoBehaviour
{
    private const float topUIMargin = 0.5f;
    [SerializeField] private SpriteRenderer sprite;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = transform;
    }

    private void Start()
    {
        PositionCameraWithTopMargin();
        ZoomCameraToSprite();
    }

    private void PositionCameraWithTopMargin()
    {
        Vector3 spritePosition = sprite.transform.position;
        cameraTransform.position = new Vector3(spritePosition.x, spritePosition.y + topUIMargin, cameraTransform.position.z);
    }

    private void ZoomCameraToSprite()
    {
        Vector3 spriteExtents = sprite.GetComponent<SpriteRenderer>().bounds.extents;
        GetComponent<Camera>().orthographicSize = spriteExtents.y + topUIMargin;
    }
}
