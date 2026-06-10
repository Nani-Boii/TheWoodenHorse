using UnityEngine;

public class VoidHandler : MonoBehaviour
{
    [SerializeField] private KeyCode manualResetKey = KeyCode.R; 
    [SerializeField] private float voidThreshold = -40f; 
    private Vector3 initialSpawn;
    private Health playerHealth;
    private Rigidbody2D rb;

    void Start()
    {
        initialSpawn = transform.position;
        playerHealth = GetComponent<Health>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (transform.position.y < voidThreshold || Input.GetKeyDown(manualResetKey))
        {
            ExecuteResetWithPenalty();
        }
    }

    public void ExecuteResetWithPenalty()
    {
        if (playerHealth != null) 
        {
            // Use the changeable value from the Health script settings[cite: 3, 4]
            playerHealth.takeDamage(playerHealth.resetDamagePenalty); 
        }
        TeleportToSafety(); 
    }

    public void TeleportToSafety()
    {
        Vector3 targetPos = initialSpawn;
        if (TeleportationShrine.currentResetShrine != null)
        {
            targetPos = TeleportationShrine.currentResetShrine.transform.position;
        }

        if (rb != null)
        {
            rb.simulated = false; 
            transform.position = targetPos;
            rb.linearVelocity = Vector2.zero; 
            rb.simulated = true;
        }
    }
}