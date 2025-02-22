using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform cameraTransform;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - cameraTransform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offset.y, transform.position.z);
    }
}

