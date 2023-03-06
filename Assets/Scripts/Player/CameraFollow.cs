using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform targetToFollow;

    // Follow Player while staying in bound of levels...
    private void LateUpdate()
    {
        transform.position = new Vector3(
            Mathf.Clamp(targetToFollow.position.x, 0, 66),
            Mathf.Clamp(targetToFollow.position.y, 0, 12),
            transform.position.z);
    }
}
