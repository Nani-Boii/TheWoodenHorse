using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Health : MonoBehaviour
{
    [Header("Health & Lives")]
    public int maxHearts = 5;
    public float currentHealth; 
    public int totalLives = 3;
    private int currentLives;

    [Header("Settings")]
    public float resetDamagePenalty = 1f;

    [Header("Shield Settings")]
    public bool isShielded = false; 
    [SerializeField] private Color shieldColor = new Color(0f, 0.6f, 1f, 1f); 
    private SpriteRenderer spriteRenderer;

    [Header("UI Transition")]
    public CanvasGroup blackScreenGroup; 
    public float fadeDuration = 1.5f;

    private void Awake()
    {
        // Cache the SpriteRenderer in Awake for performance
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        currentHealth = maxHearts;
        currentLives = totalLives;
        if (blackScreenGroup != null) blackScreenGroup.alpha = 0;
    }

    // Forced visual update to prevent Animation overrides
    private void LateUpdate()
    {
        if (spriteRenderer != null)
        {
            // If shielded, force the shield color. Otherwise, use white (normal).
            spriteRenderer.color = isShielded ? shieldColor : Color.white;
        }
    }

    public void Heal(float amount) => currentHealth = Mathf.Min(currentHealth + amount, maxHearts);

    public void ActivateShield() 
    {
        isShielded = true;
    }

    public void takeDamage(float amount)
    {
        if (isShielded) 
        {
            // Shield blocks the hit and then breaks
            isShielded = false;
            return; 
        }
        
        currentHealth -= amount;
        PlayerAnimationAudio pa = GetComponent<PlayerAnimationAudio>();
        if (pa != null) pa.PlayDamageSound();

        if (currentHealth <= 0) HandleDeath();
    }

    private void HandleDeath()
    {
        currentLives--;
        if (currentLives <= 0) 
        {
            StartCoroutine(FadeAndReset());
        }
        else
        {
            currentHealth = maxHearts;
            VoidHandler vh = GetComponent<VoidHandler>();
            if (vh != null) vh.TeleportToSafety(); 
        }
    }

    private IEnumerator FadeAndReset()
    {
        float elapsed = 0;
        if (blackScreenGroup != null) blackScreenGroup.blocksRaycasts = true;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            if (blackScreenGroup != null) blackScreenGroup.alpha = elapsed / fadeDuration;
            yield return null; 
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}