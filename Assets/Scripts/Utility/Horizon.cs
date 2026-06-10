using UnityEngine;

public class HorizontalOnlyFollow : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float parallaxMultiplier = 0.5f; 
    
    private Transform cam;
    private float initialY;

    void Start()
    {
        cam = Camera.main.transform;
        // Lock the Y position to where the object is currently placed in the scene
        initialY = transform.position.y; 
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // Follow the Camera's X position, but ignore its Y position entirely
        float targetX = cam.position.x * parallaxMultiplier;
        
        transform.position = new Vector3(targetX, initialY, transform.position.z);
    }
}