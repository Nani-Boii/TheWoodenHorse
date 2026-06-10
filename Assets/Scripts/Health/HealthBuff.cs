using UnityEngine;
using System.Collections;

public class HealthBuff : MonoBehaviour
{
    [Header("Heal Settings")]
    [SerializeField] private float healAmount = 2f; 
    [SerializeField] private float cooldownTime = 15f; 
    [SerializeField] private float inactiveAlpha = 0.3f; 

    private SpriteRenderer spriteRenderer;
    private Collider2D pickupCollider;
    private bool isReady = true;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        pickupCollider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object touching the buff is the Player[cite: 6, 8]
        if (isReady && collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            if (playerHealth != null)
            {
                // Execute the heal
                playerHealth.Heal(healAmount);
                
                // Start the same cooldown logic used in DoubleJumpBuff[cite: 6]
                StartCoroutine(StartCooldown());
            }
        }
    }

    private IEnumerator StartCooldown()
    {
        isReady = false;
        pickupCollider.enabled = false; 

        // Apply transparency feedback[cite: 6]
        Color tempColor = spriteRenderer.color;
        tempColor.a = inactiveAlpha;
        spriteRenderer.color = tempColor;

        yield return new WaitForSeconds(cooldownTime);

        // Reset the buff to active state[cite: 6]
        tempColor.a = 1f;
        spriteRenderer.color = tempColor;
        pickupCollider.enabled = true;
        isReady = true;
    }
}