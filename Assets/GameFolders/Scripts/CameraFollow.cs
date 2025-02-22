using Assets.GameFolders.Interfaces;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.2f;
    private float highestY;

    void Start()
    {
        highestY = target.position.y + 10f;
    }

    void Update()
    {
        if (target.position.y > highestY)
        {
            highestY = target.position.y;
            Vector3 newPosCam = new Vector3(transform.position.x, highestY, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosCam, smoothSpeed);
        }
    }
}

