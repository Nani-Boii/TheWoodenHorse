using UnityEngine;

public class InfiniteHorizon : MonoBehaviour
{
    [Header("Conveyor Settings")]
    public float scrollSpeed = 0.05f;
    
    private Material mat;
    private Transform cam;
    private float initialY;

    void Start()
    {
        // Find camera and cache material
        if (Camera.main != null) cam = Camera.main.transform;
        mat = GetComponent<SpriteRenderer>().material;
        
        // Lock the height
        initialY = transform.position.y;
    }

    void LateUpdate()
    {
        if (cam == null) return;

        // 1. Force the object to follow the camera (The "Belt")
        transform.position = new Vector3(cam.position.x, initialY, transform.position.z);

        // 2. Force the texture to scroll (The "Conveyor")
        // We use _MainTex because your shader doesn't have _BaseMap
        float offset = cam.position.x * scrollSpeed;
        mat.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}