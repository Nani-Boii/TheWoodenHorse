using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Follow Settings")]
    [SerializeField] private Transform target;
    [SerializeField] private float smoothSpeed = 0.125f;
    [SerializeField] private Vector3 offset;

    [Header("Dead Zone Settings")]
    [SerializeField] private float verticalDeadZone = 3.0f; 
    private float targetY;

    private void Start()
    {
        if (target == null) return;
        targetY = target.position.y + offset.y;
        
        if (offset == Vector3.zero)
            offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        float desiredY = target.position.y + offset.y;

        // Update targetY only if King leaves the vertical dead zone
        if (Mathf.Abs(desiredY - targetY) > verticalDeadZone)
        {
            if (desiredY > targetY)
                targetY = desiredY - verticalDeadZone;
            else
                targetY = desiredY + verticalDeadZone;
        }

        Vector3 finalPos = new Vector3(target.position.x + offset.x, targetY, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, finalPos, smoothSpeed);
    }
}