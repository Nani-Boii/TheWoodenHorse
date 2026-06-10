using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    private Vector3 offset;

    void Start()
    {
        offset = transform.position - playerTransform.position;
    }

    void LateUpdate()
    {
        transform.position = playerTransform.position + offset;
    }
}