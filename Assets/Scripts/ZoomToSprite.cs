using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ZoomToSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = transform;
    }

    private void Start()
    {
        CenterCameraOnSprite();
        ZoomCameraToSprite();
    }

    private void CenterCameraOnSprite()
    {
        Vector3 spritePosition = sprite.transform.position;
        cameraTransform.position = new Vector3(spritePosition.x, spritePosition.y, cameraTransform.position.z);
    }

    private void ZoomCameraToSprite()
    {
        Vector3 spriteExtents = sprite.GetComponent<SpriteRenderer>().bounds.extents;
        GetComponent<Camera>().orthographicSize = spriteExtents.y;
    }
}
