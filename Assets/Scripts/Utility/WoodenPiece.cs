using UnityEngine;

public class WoodenPiece : MonoBehaviour
{
    private bool isCollected = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ensure only the player triggers this once
        if (other.CompareTag("Player") && !isCollected)
        {
            isCollected = true;
            
            Player player = other.GetComponent<Player>();
            HorseProgression horse = Object.FindAnyObjectByType<HorseProgression>();
            CameraPan camPan = Object.FindAnyObjectByType<CameraPan>();

            if (player != null && horse != null && camPan != null)
            {
                // FIX: Removed the line that was giving you the double jump
                
                // Triggers the CameraPan sequence using the correct method
                camPan.StartPanSequence(horse.transform, horse);

                // Disable visuals and collision immediately so it "disappears"
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<Collider2D>().enabled = false;
                
                // Fully remove the object after the cinematic time
                Destroy(gameObject, 10f); 
            }
        }
    }
}