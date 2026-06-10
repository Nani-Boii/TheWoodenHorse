using UnityEngine;

public class Damage : MonoBehaviour
{
    [SerializeField] private float damageAmount = 1f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            Health playerHealth = other.GetComponent<Health>();

            // IMPORTANT: Only apply damage if the player's iFrames are NOT active
            if (player != null && playerHealth != null && !player.IsInvincible())
            {
                playerHealth.takeDamage(damageAmount);
                player.ApplyKnockback(transform.position);
            }
        }
    }
}