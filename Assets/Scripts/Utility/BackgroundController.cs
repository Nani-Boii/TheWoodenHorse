using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    [Header("References")]
    public GameObject cam;
    public Transform[] backgroundParts; // Drag your 3-5 images here in order
    
    [Header("Settings")]
    public float parallaxEffect; // 0 is follow cam, 1 is static

    private float startPos;
    private float backgroundWidth;
    private int leftIndex;
    private int rightIndex;

    void Start()
    {
        // 1. Capture the initial offset to prevent the "jump" at start
        startPos = transform.position.x - (cam.transform.position.x * parallaxEffect);

        // 2. Calculate the width based on the first piece
        if (backgroundParts.Length > 0)
        {
            backgroundWidth = backgroundParts[0].GetComponent<SpriteRenderer>().bounds.size.x;
        }
        
        leftIndex = 0;
        rightIndex = backgroundParts.Length - 1;
    }

    void Update()
    {
        // 3. Handle Parallax Movement for the whole group
        float distance = (cam.transform.position.x * parallaxEffect);
        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);

        // 4. Check if we need to recycle images (Infinite Looping)
        if (cam.transform.position.x > (backgroundParts[rightIndex].transform.position.x))
        {
            ScrollRight();
        }
        else if (cam.transform.position.x < (backgroundParts[leftIndex].transform.position.x))
        {
            ScrollLeft();
        }
    }

    private void ScrollRight()
    {
        // Move the leftmost piece to the right of the rightmost piece
        float newX = backgroundParts[rightIndex].position.x + backgroundWidth;
        backgroundParts[leftIndex].position = new Vector3(newX, backgroundParts[leftIndex].position.y, backgroundParts[leftIndex].position.z);

        rightIndex = leftIndex;
        leftIndex++;

        if (leftIndex == backgroundParts.Length)
            leftIndex = 0;
    }

    private void ScrollLeft()
    {
        // Move the rightmost piece to the left of the leftmost piece
        float newX = backgroundParts[leftIndex].position.x - backgroundWidth;
        backgroundParts[rightIndex].position = new Vector3(newX, backgroundParts[rightIndex].position.y, backgroundParts[rightIndex].position.z);

        leftIndex = rightIndex;
        rightIndex--;

        if (rightIndex < 0)
            rightIndex = backgroundParts.Length - 1;
    }
}