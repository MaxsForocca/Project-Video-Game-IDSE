using UnityEngine;

public class CameraFollow2_5D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Limits (X Axis)")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [Header("Limits (Y Axis)")]
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    [Header("Camera Settings")]
    [SerializeField] private float fixedZ = -10f;
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (player == null) return;

        float targetX = Mathf.Clamp(player.position.x, minX, maxX);
        float targetY = Mathf.Clamp(player.position.y, minY, maxY);

        Vector3 targetPosition = new Vector3(
            targetX,
            targetY,
            fixedZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
