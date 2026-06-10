using UnityEngine;
using System.Collections;

public class DoubleJumpBuff : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 30f; 
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
            Player player = collision.GetComponent<Player>();
            if (player != null)
            {
                // CHANGED: This now matches the new method name in Player.cs
                player.EnablePermanentDoubleJump(); 
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