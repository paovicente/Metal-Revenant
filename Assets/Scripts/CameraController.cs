using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Target")]
    public Transform target;

    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(2f, 2f, -10f);
    [Range(0.0f, 0.5f)] public float smoothTime = 0.05f;

    private Vector3 velocity = Vector3.zero;

    //we use LateUpdate because first the player moves and then the camera follow him
    private void LateUpdate()
    {
        if (target == null)
            return;

        Vector3 targetPos = target.position + offset;

        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smoothTime);
    }
}
