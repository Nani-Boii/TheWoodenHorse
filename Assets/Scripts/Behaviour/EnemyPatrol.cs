using UnityEngine;

public class SimplePatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    public Transform pointA;
    public Transform pointB;
    [SerializeField] public float speed = 2f;

    private Transform currentTarget;
    private Vector3 originalScale; // Stores the size set in the Inspector

    private void Start()
    {
        // Save the current scale so we can reuse it later
        originalScale = transform.localScale;

        // Start by heading toward Point B
        if (pointB != null) 
        {
            currentTarget = pointB;
        }
    }

    private void Update() 
    {
        if (pointA == null || pointB == null) return;

        // Move the enemy towards the target position
        transform.position = Vector2.MoveTowards(
            transform.position, 
            currentTarget.position, 
            speed * Time.deltaTime
        );

        // Check if we arrived at the point
        if (Vector2.Distance(transform.position, currentTarget.position) < 0.1f) 
        {
            currentTarget = (currentTarget == pointA) ? pointB : pointA;
            Flip();
        }
    }

    private void Flip()
    {
        // Use the absolute value of the original scale to ensure it's always positive,
        // then apply the negative sign to flip only the X-axis direction.
        if (currentTarget == pointB)
        {
            // Face "original" direction
            transform.localScale = new Vector3(Mathf.Abs(originalScale.x), originalScale.y, originalScale.z); 
        }
        else
        {
            // Face "opposite" direction
            transform.localScale = new Vector3(-Mathf.Abs(originalScale.x), originalScale.y, originalScale.z);
        }
    }
}