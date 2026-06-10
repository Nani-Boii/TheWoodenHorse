using UnityEngine;

public class SingleParallax : MonoBehaviour
{
    [Header("Assign Camera")]
    public Transform cam; 

    [Header("Parallax Speed (Texture Scroll)")]
    [Range(0f, 1f)] public float parallaxSpeedX = 0.1f; 
    [Range(0f, 1f)] public float parallaxSpeedY = 0.05f; 

    [Header("Position Offset")]
    public Vector2 positionOffset;

    private Material mat;
    private string textureProperty = "_MainTex"; 

    void Start()
    {
        if (cam == null) cam = Camera.main.transform;
        
        mat = GetComponent<Renderer>().material;

        // Check for URP compatibility
        if (mat.HasProperty("_BaseMap")) textureProperty = "_BaseMap";
    }

    private void LateUpdate()
    {
        if (cam == null) return;

        // 1. Calculate texture offset based on camera position
        float offsetX = cam.position.x * parallaxSpeedX;
        float offsetY = cam.position.y * parallaxSpeedY;

        // 2. LOCK POSITION: This makes the BG follow the camera everywhere
        transform.position = new Vector3(cam.position.x + positionOffset.x, cam.position.y + positionOffset.y, transform.position.z);

        // 3. APPLY TEXTURE SLIDE
        mat.SetTextureOffset(textureProperty, new Vector2(offsetX, offsetY));
    }
}