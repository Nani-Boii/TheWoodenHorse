using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [Header("Target Settings")]
    // Drag the Boulder GameObject into this slot in the Inspector
    [SerializeField] public GameObject targetObject; 
    
    // How transparent the object should become (0.0 = invisible, 1.0 = solid)
    [Range(0f, 1f)]
    [SerializeField] public float targetAlpha = 0.3f; 

    [Header("Switch Visuals (Optional)")]
    // If you want the switch to change appearance when flipped
    [SerializeField] private Sprite activeSwitchSprite; 
    private SpriteRenderer switchSpriteRenderer;

    private void Start()
    {
        switchSpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object entering the trigger has the "Player" tag
        if (other.CompareTag("Player"))
        {
            ActivateSwitch();
        }
    }

    private void ActivateSwitch()
    {
        // If a target object was assigned, modify its collider and sprite
        if (targetObject != null)
        {
            // 1. Disable the Target's Collider
            Collider2D targetCollider = targetObject.GetComponent<Collider2D>();
            if (targetCollider != null)
            {
                targetCollider.enabled = false;
            }

            // 2. Make the Target transparent
            SpriteRenderer targetSprite = targetObject.GetComponent<SpriteRenderer>();
            if (targetSprite != null)
            {
                Color currentColor = targetSprite.color;
                currentColor.a = targetAlpha;
                targetSprite.color = currentColor;
            }
            
            Debug.Log(targetObject.name + " has been disabled and faded by the switch!");
        }

        // Optional: Change the visual look of the switch itself
        if (activeSwitchSprite != null && switchSpriteRenderer != null)
        {
            switchSpriteRenderer.sprite = activeSwitchSprite;
        }

        // Disable this switch trigger collider so it can't be triggered repeatedly
        GetComponent<Collider2D>().enabled = false;
    }
}