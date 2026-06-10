using UnityEngine;

public class SmoothBobber : MonoBehaviour
{
    [Header("Bobbing Settings")]
    [SerializeField] private float amplitude = 0.5f; // How high and low it goes
    [SerializeField] private float frequency = 1f;   // How fast it moves

    [Header("Optional Effects")]
    [SerializeField] private float rotateSpeed = 0f; // Set above 0 to make it spin while bobbing

    private Vector3 startPosition;

    void Start()
    {
        // Store the starting position of the object
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using Mathf.Sin
        float newY = startPosition.y + Mathf.Sin(Time.time * frequency) * amplitude;

        // Apply the new position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);

        // If rotate speed is set, spin the object for a nice visual touch (e.g., a collectible item)
        if (rotateSpeed > 0)
        {
            transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        }
    }
}