using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Offset")]
    public Vector3 offset = new Vector3(0, 0, -10);

    [Header("Smoothing")]
    public float smoothSpeed = 5f;

    [Header("Axis")]
    public bool followX = true;
    public bool followY = true;

    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 desiredPosition =
            target.position + offset;

        Vector3 currentPosition =
            transform.position;

        if (!followX)
            desiredPosition.x =
                currentPosition.x;

        if (!followY)
            desiredPosition.y =
                currentPosition.y;

        transform.position =
            Vector3.Lerp(
                currentPosition,
                desiredPosition,
                smoothSpeed *
                Time.deltaTime
            );
    }
}