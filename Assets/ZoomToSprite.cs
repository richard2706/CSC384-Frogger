using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomToSprite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    private Transform cameraTransform;
    private float cameraZ;

    private void Awake()
    {
        cameraTransform = transform;
        cameraZ = transform.position.z;
    }

    private void Start()
    {
        Vector3 spritePosition = sprite.transform.position;
        cameraTransform.position = new Vector3(spritePosition.x, spritePosition.y, cameraZ);
    }
}
