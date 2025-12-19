using UnityEngine;

public class CameraFollow2_5D : MonoBehaviour
{
    [Header("Target")]
    [SerializeField] private Transform player;

    [Header("Limits (X Axis)")]
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    [Header("Camera Settings")]
    [SerializeField] private float fixedY = 0f;
    [SerializeField] private float fixedZ = -10f;
    [SerializeField] private float smoothSpeed = 5f;

    private void LateUpdate()
    {
        if (player == null) return;

        float targetX = Mathf.Clamp(player.position.x, minX, maxX);

        Vector3 targetPosition = new Vector3(
            targetX,
            fixedY,
            fixedZ
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}
