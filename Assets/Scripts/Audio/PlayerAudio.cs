using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Audio Clips")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip damageSound;

    private AudioSource audioSource;
    private bool isGrounded;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Call this in your Jump function
    public void PlayJump()
    {
        audioSource.PlayOneShot(jumpSound);
    }

    // Call this inside your Collision/Grounded logic
    public void PlayLanding()
    {
        audioSource.PlayOneShot(landSound);
    }

    // Call this in your TakeDamage function
    public void PlayDamage()
    {
        audioSource.PlayOneShot(damageSound);
    }
}