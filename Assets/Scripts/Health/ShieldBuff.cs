using UnityEngine;
using System.Collections;

public class ShieldBuff : MonoBehaviour
{
    [Header("Shield Settings")]
    [SerializeField] private float cooldownTime = 20f; 
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
        if (isReady && collision.CompareTag("Player"))
        {
            Health playerHealth = collision.GetComponent<Health>();
            // Only pick up if the player doesn't already have a shield
            if (playerHealth != null && !playerHealth.isShielded)
            {
                playerHealth.ActivateShield();
                StartCoroutine(StartCooldown());
            }
        }
    }

    private IEnumerator StartCooldown()
    {
        isReady = false;
        pickupCollider.enabled = false; 

        Color tempColor = spriteRenderer.color;
        tempColor.a = inactiveAlpha;
        spriteRenderer.color = tempColor;

        yield return new WaitForSeconds(cooldownTime);

        tempColor.a = 1f;
        spriteRenderer.color = tempColor;
        pickupCollider.enabled = true;
        isReady = true;
    }
}