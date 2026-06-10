using UnityEngine;

public class TeleportationShrine : MonoBehaviour
{
    [Header("Teleport Settings")]
    public TeleportationShrine destinationShrine; 
    public KeyCode teleportKey = KeyCode.E;   
    public KeyCode claimKey = KeyCode.F; 

    [Header("Visual Feedback")]
    public Sprite normalSprite;   
    public Sprite crystalSprite;  
    public Color unclaimedColor = Color.gray;
    public Color individualClaimedColor = Color.yellow; 
    public Color fullyActiveColor = Color.cyan;      

    public bool isThisShrineClaimed = false; 
    private bool isPlayerInside = false;
    private SpriteRenderer spriteRenderer;

    // Static reference for the R key reset point
    public static TeleportationShrine currentResetShrine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisuals();
    }

    void Update()
    {
        // System is ready if both this and the target are claimed
        bool systemReady = isThisShrineClaimed && destinationShrine != null && destinationShrine.isThisShrineClaimed;

        if (isPlayerInside)
        {
            // E: Teleport to the other side (Back and Forth)
            if (systemReady && Input.GetKeyDown(teleportKey))
            {
                TeleportPlayer();
            }

            // F: Toggle Reset Point[cite: 6]
            if (Input.GetKeyDown(claimKey))
            {
                if (currentResetShrine == this)
                {
                    // If already the reset point, remove it
                    currentResetShrine = null;
                    Debug.Log("<color=red>Reset point removed!</color>");
                }
                else
                {
                    // Set as the new reset point
                    currentResetShrine = this;
                    Debug.Log("<color=green>Reset point set to this shrine!</color>");
                }
            }
        }

        UpdateSpriteDesign();
    }

    private void UpdateSpriteDesign()
    {
        if (spriteRenderer == null) return;

        // Visual toggle: Crystal if it's the reset point, Normal if not[cite: 6]
        if (currentResetShrine == this)
        {
            if (crystalSprite != null) spriteRenderer.sprite = crystalSprite;
        }
        else
        {
            if (normalSprite != null) spriteRenderer.sprite = normalSprite;
        }
    }

    public void UpdateVisuals()
    {
        if (spriteRenderer == null) return;

        bool bothClaimed = isThisShrineClaimed && destinationShrine != null && destinationShrine.isThisShrineClaimed;

        if (bothClaimed) spriteRenderer.color = fullyActiveColor;
        else if (isThisShrineClaimed) spriteRenderer.color = individualClaimedColor;
        else spriteRenderer.color = unclaimedColor;
    }

    private void TeleportPlayer()
    {
        Player player = Object.FindFirstObjectByType<Player>();
        if (player != null && destinationShrine != null)
        {
            Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.simulated = false; 
                player.transform.position = destinationShrine.transform.position;
                rb.linearVelocity = Vector2.zero; 
                rb.simulated = true;
                Debug.Log("<color=cyan>Teleported back and forth!</color>");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInside = true;
            if (!isThisShrineClaimed)
            {
                isThisShrineClaimed = true; 
                UpdateVisuals(); 
                if (destinationShrine != null) destinationShrine.UpdateVisuals();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) isPlayerInside = false;
    }
}